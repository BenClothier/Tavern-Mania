using Pathfinding;
using System.Collections;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private bool activeAI = true;
    [SerializeField] private EventChannel_Customer onCustomerLeave;
    [SerializeField] private ParticleSystem lifeLossEffect;

    [Header("Patience")]
    [SerializeField] private float startPatience;
    [SerializeField] private float maxPatience;
    [SerializeField] private FloatVariable patienceDropMultiplier;
    [SerializeField] private FloatVariable lowestPatienceVar;
    [SerializeField] private AnimationCurve patienceProportionToAnger;
    [SerializeField] private float angerReturnTime = 1.5f;

    private LevelController levelController;
    private CustomerSpawn spawn;

    private AIPath navigation;
    private Animator animator;
    private SpriteRenderer sr;
    private CircleCollider2D col;

    public CustomerState CurrentState { get; private set; }

    public float CurrentPatience { get; private set; }

    private void Awake()
    {
        navigation = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();

        levelController = FindObjectOfType<LevelController>();
        spawn = FindObjectOfType<CustomerSpawn>();

        CurrentPatience = startPatience;

        if (!activeAI)
        {
            animator.enabled = false;
            StartCoroutine(ManualPatienceClock());
        }
    }

    void Start()
    {
        if (activeAI)
        {
            CurrentState = CustomerState.Waiting;
            FindBar(out Bar bar);
            if (!bar.JoinQueue(this))
            {
                GoToBar(bar);
            }
        }
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", navigation.velocity.x);
        animator.SetFloat("Vertical", navigation.velocity.y);
        sr.material.SetFloat("_Anger", patienceProportionToAnger.Evaluate(CurrentPatience / maxPatience));
        sr.material.SetFloat("_UnscaledTime", Time.unscaledTime);
    }

    public void EnableAI()
    { 
        if (!activeAI)
        {
            activeAI = true;
            animator.enabled = true;
            StopAllCoroutines();
            Start();
        }
        else
        {
            Debug.LogWarning("AI already active");
        }
    }

    public void InviteToBar(Bar bar)
    {
        if (CurrentState == CustomerState.Waiting)
        {
            CurrentState = CustomerState.BarSecured;
            GoToBar(bar);
        }
        else
        {
            Debug.LogError("Can't be invited to bar unless they are waiting!");
        }
    }

    protected virtual void OnOrderSatisfied(Bar bar, DrinkMix drinkMix)
    {
        if (CurrentState == CustomerState.Ordered)
        {
            CurrentState = CustomerState.Leaving;
            StartCoroutine(ReturnAngerToZero());
            Leave();
        }
        else
        {
            Debug.LogError("Customer had no order to satisfy!");
        }
    }

    private bool FindBar(out Bar bar)
    {
        Bar[] bars = FindObjectsOfType<Bar>();

        var freeBars = bars.Where(bar => bar.IsVacant);

        if (freeBars.Count() < 1)
        {
            bar = bars[UnityEngine.Random.Range(0, bars.Length)];
            return false;
        }
        else
        {
            bar = freeBars.ElementAt(UnityEngine.Random.Range(0, freeBars.Count()));
            return true;
        }
    }

    private void GoToBar(Bar bar)
    {
        if (CurrentState == CustomerState.BarSecured)
        {
            col.enabled = false;
            navigation.destination = bar.transform.position - new Vector3(0, 0.5f + navigation.radius, 0);
            navigation.OnTargetReachedEvent += () => MakeOrder(bar);
        }
        else
        {
            navigation.destination = bar.transform.position - new Vector3(UnityEngine.Random.Range(-.5f,.5f), 0.5f + navigation.radius * 2, 0);
        }
    }

    private void MakeOrder(Bar bar)
    {
        if (CurrentState == CustomerState.BarSecured)
        {
            CurrentState = CustomerState.Ordered;
            col.enabled = true;
            navigation.ClearOnTargetReachedListener();
            bar.PlaceOrder(levelController.GenerateOrder(this));
            bar.ThisOrderSatisfied += (drink) => OnOrderSatisfied(bar, drink);
            navigation.canSearch = false;

            StartCoroutine(PatienceClock(bar));
        }
        else
        {
            Debug.LogError("Customer can't place an order while not at a bar!");
        }
    }

    private IEnumerator PatienceClock(Bar bar)
    {
        CurrentPatience = startPatience;

        while (CurrentPatience > 0 && CurrentState == CustomerState.Ordered)
        {
            yield return null;
            CurrentPatience = Mathf.Clamp(CurrentPatience - patienceDropMultiplier.Value * Time.deltaTime, 0, maxPatience);

            if (CurrentState == CustomerState.Ordered && CurrentPatience < lowestPatienceVar.Value)
            {
                lowestPatienceVar.Value = CurrentPatience;
            }
        }

        if (CurrentState == CustomerState.Ordered && CurrentPatience <= 0 && bar != null)
        {
            OnOrderFailed(bar);
        }
    }

    private IEnumerator ManualPatienceClock()
    {
        CurrentPatience = startPatience;

        while (CurrentPatience > 0 && !activeAI)
        {
            yield return null;
            CurrentPatience = Mathf.Clamp(CurrentPatience - patienceDropMultiplier.Value * Time.deltaTime, 0, maxPatience);
        }
    }

    private void OnOrderFailed(Bar bar)
    {
        if (CurrentState == CustomerState.Ordered)
        {
            CurrentState = CustomerState.Leaving;
            bar.FailOrder();
            lifeLossEffect.Play();

            if (levelController.GameOver)
            {
                sr.material.SetInt("_UseUnscaledTime", 1);
            }

            Leave();
        }
        else
        {
            Debug.LogError("Customer had no order to satisfy!");
        }
    }

    private void Leave()
    {
        lowestPatienceVar.Value = startPatience;
        col.enabled = false;
        navigation.canSearch = true;
        navigation.destination = spawn.transform.position;
        navigation.OnTargetReachedEvent += DestroySafely;
    }

    private void DestroySafely()
    {
        onCustomerLeave.Invoke(this);
        StopAllCoroutines();
        navigation.ClearOnTargetReachedListener();
        Destroy(gameObject);
    }

    private IEnumerator ReturnAngerToZero()
    {
        float finalAnger = patienceProportionToAnger.Evaluate(CurrentPatience / startPatience);
        float newAnger = finalAnger;
        float timePassed = 0;

        while (newAnger > 0)
        {
            timePassed += Time.deltaTime;
            newAnger = Mathf.Lerp(finalAnger, 0, timePassed / angerReturnTime);
            sr.material.SetFloat("_Anger", newAnger);
            yield return null;
        }
    }

    public enum CustomerState
    {
        Waiting,
        BarSecured,
        Ordered,
        Leaving,
    }
}

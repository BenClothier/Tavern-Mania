using Pathfinding;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerState CurrentState { get; private set; }

    private AIPath navigation;
    private Animator animator;
    private Rigidbody2D rb;
    private CircleCollider2D col;

    private LevelController levelController;

    private void Awake()
    {
        navigation = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();

        levelController = FindObjectOfType<LevelController>();
    }

    void Start()
    {
        CurrentState = CustomerState.Waiting;
        FindBar(out Bar bar);
        if (!bar.JoinQueue(this))
        {
            GoToBar(bar);
        }
    }

    private void Update()
    {
        animator.SetFloat("Horizontal", navigation.velocity.x);
        animator.SetFloat("Vertical", navigation.velocity.y);
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
        col.enabled = true;
        navigation.ClearOnTargetReachedListener();
        bar.PlaceOrder(levelController.GenerateOrder(this));
        bar.OrderSatisfied += (drink) => OnOrderSatisfied(bar, drink);
        navigation.canSearch = false;
    }

    private void OnOrderSatisfied(Bar bar, DrinkMix drinkMix)
    {
        col.enabled = false;
        navigation.canSearch = true;
        navigation.destination = new Vector2(0, navigation.radius);
        navigation.OnTargetReachedEvent += DestroySafely;
    }

    private void DestroySafely()
    {
        navigation.ClearOnTargetReachedListener();
        Destroy(gameObject);
    }

    public enum CustomerState
    {
        Waiting,
        BarSecured,
        AtBar,
        Ordered,
        Leaving,
    }
}

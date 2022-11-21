using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private DrinkHeld drinkHeld;
    [SerializeField] private IntVariable livesRemaining;

    [Header("Intensity Calculation")]
    [SerializeField] private FloatVariable patienceDropMultiplierVar;
    [SerializeField] private FloatVariable lowestPatienceVar;
    [SerializeField] private AnimationCurve intensityValueByAveragePatience;
    [SerializeField] private AnimationCurve intensityMultiplierByLowestPatience;
    [SerializeField] private AnimationCurve intensityMultiplierByPatienceDropMultiplier;
    [SerializeField] private float intensityFollowSpeed = 0.3f;

    [Header("Events")]
    [SerializeField] private EventChannel_GameOverInfo onGameOver;
    [SerializeField] private EventChannel_Void onLevelComplete;
    [SerializeField] private EventChannel_Vector2 onOrderFailed;
    [SerializeField] private EventChannel_Customer onCustomerLeave;

    [Header("Game-wide Variables")]
    [SerializeField] private IntVariable livesPerLevel;

    private CustomerSpawn customerSpawn;

    private Stack<int> orderStack;
    private Stack<CustomerType> customerStack;

    private float targetIntensity;
    private int customerCount;

    private List<Customer> customers = new List<Customer>();

    private enum CustomerType
    {
        Normal,
        Magic,
    }

    public bool GameOver { get; private set; }

    public bool LevelComplete { get; private set; }

    public float Intensity { get; private set; }

    public LevelSettings LevelSettings => levelSettings;

    private void Awake()
    {
        var rnd = new System.Random();

        // Reset game variables for level
        drinkHeld.EmptyGlass();
        lowestPatienceVar.Value = 1000;
        livesRemaining.Value = livesPerLevel.Value;

        // Initialise listeners
        onOrderFailed.OnEventInvocation += LoseLife;
        onCustomerLeave.OnEventInvocation += OnCustomerLeave;

        // Initalise barrels with liquid
        Barrel[] barrels = FindObjectsOfType<Barrel>();

        Assert.IsTrue(barrels.Length == levelSettings.liquidsAvailable.Count, "Barrel count does not match liquid count!!");
        Assert.IsTrue((levelSettings.maxSingleOrders + levelSettings.maxDoubleOrders + levelSettings.maxTripleOrders) >= levelSettings.customerCount, "Order count must be at least the same as the customer count!!");

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Liquid = levelSettings.liquidsAvailable[i];
        }

        // Pre-generate sequence of orders
        List<int> orders = new List<int>();
        orders.AddRange(Enumerable.Repeat(1, levelSettings.maxSingleOrders));
        orders.AddRange(Enumerable.Repeat(2, levelSettings.maxDoubleOrders));
        orders.AddRange(Enumerable.Repeat(3, levelSettings.maxTripleOrders));
        orderStack = new Stack<int>(orders.OrderBy(x => rnd.Next()).ToArray());

        // Pre-generate sequence of customers
        List<CustomerType> customers = new List<CustomerType>();
        customers.AddRange(Enumerable.Repeat(CustomerType.Normal, levelSettings.customerCount));
        customers.AddRange(Enumerable.Repeat(CustomerType.Magic, levelSettings.magicCustomerCount));
        customerStack = new Stack<CustomerType>(customers.OrderBy(x => rnd.Next()).ToArray());

        customerSpawn = FindObjectOfType<CustomerSpawn>();
        patienceDropMultiplierVar.Value = levelSettings.patienceDropMultiplier;

        StartCoroutine(SpawnCustomerRoutine());
    }

    private void OnDisable()
    {
        onOrderFailed.OnEventInvocation -= LoseLife;
        onCustomerLeave.OnEventInvocation -= OnCustomerLeave;
    }

    private void Update()
    {
        targetIntensity =
            (customers.Count < 1 ? 0 : intensityValueByAveragePatience.Evaluate(customers.Average(c => c.CurrentPatience)))
            * intensityMultiplierByPatienceDropMultiplier.Evaluate(patienceDropMultiplierVar.Value)
            * intensityMultiplierByLowestPatience.Evaluate(lowestPatienceVar.Value);

        Intensity = Mathf.Lerp(Intensity, targetIntensity, intensityFollowSpeed * Time.unscaledDeltaTime);
    }

    public DrinkMix GenerateOrder(Customer customer)
    {
        List<Liquid> liquids = new ();
        int liquidCount = orderStack.Pop();

        for (int i = 0; i < liquidCount; i++)
        {
            liquids.Add(levelSettings.liquidsAvailable[UnityEngine.Random.Range(0, levelSettings.liquidsAvailable.Count)]);
        }

        customers.Add(customer);

        return new DrinkMix(liquids);
    }

    private void LoseLife(Vector2 loseOrderPosition)
    {
        livesRemaining.Value--;

        if (livesRemaining.Value < 1)
        {
            GameOver = true;
            onGameOver.Invoke(new GameOverInfo(loseOrderPosition));
            Time.timeScale = 0;
        }
    }

    private void OnCustomerLeave(Customer customer)
    {
        customerCount--;
        customers.Remove(customer);
        CheckForVictory();
    }

    private void CheckForVictory()
    {
        if (customerStack.Count < 1 && customerCount < 1)
        {
            LevelComplete = true;
            PlayLevel.levelComplete(1);
            StartCoroutine(VictoryDelay());
        }
    }

    private IEnumerator VictoryDelay()
    {
        yield return new WaitForSeconds(levelSettings.victoryDelay);
        onLevelComplete.Invoke();
        Time.timeScale = 0;
    }

    private IEnumerator SpawnCustomerRoutine()
    {
        while (!GameOver && !LevelComplete && customerStack.Count > 0)
        {
            yield return new WaitForSeconds(levelSettings.customerSpawnPeriodCurve.Evaluate(1 - ((float)customerStack.Count / levelSettings.TotalCustomersThisLevel)));

            if (customerStack.Pop() == CustomerType.Normal)
            {
                customerSpawn.SpawnCustomer();
            }
            else
            {
                customerSpawn.SpawnMagicCustomer();
            }

            customerCount++;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.Lerp(Color.green, Color.red, Intensity / 1);
        Handles.DrawSolidDisc(Vector2.left * 5 + Vector2.up * 3, Vector3.forward, 0.2f);
        Handles.Label(Vector2.left * 5 + Vector2.up * 3.4f, $"{Intensity}");

        Handles.color = Color.Lerp(Color.green, Color.red, targetIntensity / 1);
        Handles.DrawSolidDisc(Vector2.left * 5 + Vector2.up * 2, Vector3.forward, 0.2f);
        Handles.Label(Vector2.left * 5 + Vector2.up * 2.4f, $"{targetIntensity}");

        float val = customers.Count < 1 ? 0 : intensityValueByAveragePatience.Evaluate(customers.Average(c => c.CurrentPatience));

        if (val < 1)
        {
            Handles.color = Color.Lerp(Color.blue, Color.green, val / 1);
        }
        else
        {
            Handles.color = Color.Lerp(Color.green, Color.red, (val - 1) / 1);
        }

        Handles.DrawSolidDisc(Vector2.left * 5 + Vector2.up, Vector3.forward, 0.2f);
        Handles.Label(Vector2.left * 5 + Vector2.up * 1.4f, $"{val}");


        val = intensityMultiplierByLowestPatience.Evaluate(lowestPatienceVar.Value);

        if (val < 1)
        {
            Handles.color = Color.Lerp(Color.blue, Color.green, val / 1);
        }
        else
        {
            Handles.color = Color.Lerp(Color.green, Color.red, (val - 1) / 1);
        }

        Handles.DrawSolidDisc(Vector2.left * 6, Vector3.forward, 0.2f);
        Handles.Label(Vector2.left * 6 + Vector2.down * .4f, $"{val}");

        val = intensityMultiplierByPatienceDropMultiplier.Evaluate(patienceDropMultiplierVar.Value);

        if (val < 0)
        {
            Handles.color = Color.Lerp(Color.magenta, Color.blue, (val + 1) / 1);
        }
        else if (val < 1)
        {
            Handles.color = Color.Lerp(Color.blue, Color.green, val / 1);
        }
        else
        {
            Handles.color = Color.Lerp(Color.green, Color.red, (val - 1) / 1);
        }

        Handles.DrawSolidDisc(Vector2.left * 4, Vector3.forward, 0.2f);
        Handles.Label(Vector2.left * 4 + Vector2.down * .4f, $"{val}");
    }
#endif
}

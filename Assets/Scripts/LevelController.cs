using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private DrinkHeld drinkHeld;
    [SerializeField] private FloatVariable patienceDropMultiplierVar;

    [Header("Events")]
    [SerializeField] private EventChannel_GameOverInfo onGameOver;
    [SerializeField] private EventChannel_Void onLevelComplete;
    [SerializeField] private EventChannel_Vector2 onOrderFailed;
    [SerializeField] private EventChannel_Void onCustomerLeave;

    [Header("Game-wide Variables")]
    [SerializeField] private IntVariable livesPerLevel;

    private CustomerSpawn customerSpawn;

    private Stack<int> orderStack;
    private Stack<CustomerType> customerStack;

    private int livesRemaining;

    private enum CustomerType
    {
        Normal,
        Magic,
    }

    public bool GameOver { get; private set; }

    public bool LevelComplete { get; private set; }

    public LevelSettings LevelSettings => levelSettings;

    private void Awake()
    {
        var rnd = new System.Random();

        // Reset game variables for level
        drinkHeld.EmptyGlass();

        // Initialise listeners
        onOrderFailed.OnEventInvocation += LoseLife;
        onCustomerLeave.OnEventInvocation += CheckForVictory;

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

        // Set local variables
        livesRemaining = livesPerLevel.Value;

        StartCoroutine(SpawnCustomerRoutine());
    }

    private void OnDisable()
    {
        onOrderFailed.OnEventInvocation -= LoseLife;
        onCustomerLeave.OnEventInvocation -= CheckForVictory;
    }

    public DrinkMix GenerateOrder(Customer customer)
    {
        List<Liquid> liquids = new ();
        int liquidCount = orderStack.Pop();

        for (int i = 0; i < liquidCount; i++)
        {
            liquids.Add(levelSettings.liquidsAvailable[UnityEngine.Random.Range(0, levelSettings.liquidsAvailable.Count)]);
        }

        return new DrinkMix(liquids);
    }

    private void LoseLife(Vector2 loseOrderPosition)
    {
        livesRemaining--;

        if (livesRemaining < 1)
        {
            GameOver = true;
            onGameOver.Invoke(new GameOverInfo(loseOrderPosition));
            Time.timeScale = 0;
        }
    }

    private void CheckForVictory()
    {
        if (customerStack.Count < 1 && FindObjectsOfType<Customer>().Count() < 2)
        {
            LevelComplete = true;
            onLevelComplete.Invoke();
            Time.timeScale = 0;
        }
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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private FloatVariable patienceDropMultiplierVar;

    Stack<int> orderStack;
    Stack<CustomerType> customerStack;

    private CustomerSpawn customerSpawn;

    private enum CustomerType
    {
        Normal,
        Magic,
    }

    public bool GameOver { get; private set; }

    public LevelSettings LevelSettings => levelSettings;

    private void Awake()
    {
        Barrel[] barrels = FindObjectsOfType<Barrel>();

        Assert.IsTrue(barrels.Length == levelSettings.liquidsAvailable.Count, "Barrel count does not match liquid count!!");
        Assert.IsTrue((levelSettings.maxSingleOrders + levelSettings.maxDoubleOrders + levelSettings.maxTripleOrders) >= levelSettings.customerCount, "Order count must be at least the same as the customer count!!");

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Liquid = levelSettings.liquidsAvailable[i];
        }

        var rnd = new System.Random();

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

    private IEnumerator SpawnCustomerRoutine()
    {
        while (!GameOver && customerStack.Count > 0)
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

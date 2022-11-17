using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;
    [SerializeField] private FloatVariable patienceDropMultiplierVar;

    private int customersRemaining;
    private CustomerSpawn customerSpawn;

    public bool GameOver { get; private set; }

    private void Awake()
    {
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        Assert.IsTrue(barrels.Length == levelSettings.liquidsAvailable.Count, "Barrel count does not match liquid count!!");

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].Liquid = levelSettings.liquidsAvailable[i];
        }

        customerSpawn = FindObjectOfType<CustomerSpawn>();
        customersRemaining = levelSettings.customerCount;
        patienceDropMultiplierVar.Value = levelSettings.patienceMultiplier;

        StartCoroutine(SpawnCustomerRoutine());
    }

    public DrinkMix GenerateOrder(Customer customer)
    {
        List<Liquid> liquids = new ();
        int liquidCount = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < liquidCount; i++)
        {
            liquids.Add(levelSettings.liquidsAvailable[UnityEngine.Random.Range(0, levelSettings.liquidsAvailable.Count)]);
        }

        return new DrinkMix(liquids);
    }

    private IEnumerator SpawnCustomerRoutine()
    {
        while (!GameOver && customersRemaining > 0)
        {
            yield return new WaitForSeconds(levelSettings.customerSpawnPeriodCurve.Evaluate(1 - ((float)customersRemaining / levelSettings.customerCount)));
            customerSpawn.SpawnCustomer();
            customersRemaining--;
        }
    }
}

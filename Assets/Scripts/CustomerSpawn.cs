using UnityEngine;
using System;

public class CustomerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private AnimationCurve spawnRate;
    private float nextSpawnTime;
    [SerializeField] private int maxCustomers;
    private int numberOfCustomers;

    void Start() // change to OnGameStart listener
    {
        SpawnCustomer();
        numberOfCustomers = 0;
    }

    void Update()
    {
        if (numberOfCustomers < maxCustomers)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnCustomer();
                numberOfCustomers = numberOfCustomers + 1;
            }
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, transform.position, Quaternion.identity);
        nextSpawnTime = spawnRate.Evaluate(Time.time) + Time.time;
        Debug.Log(Time.time);
    }
}

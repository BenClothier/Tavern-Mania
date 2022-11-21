using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public int TotalCustomersThisLevel => customerCount + magicCustomerCount;

    [Header("Orders")]
    public List<Liquid> liquidsAvailable;
    public int maxSingleOrders;
    public int maxDoubleOrders;
    public int maxTripleOrders;

    [Header("Customers")]
    public int customerCount;
    public int magicCustomerCount;
    public AnimationCurve customerSpawnPeriodCurve;

    [Header("Patience")]
    public float patienceDropMultiplier = 1;
    public float patienceCalmerPatienceDropMultiplier = -0.2f;
    public float patienceCalmerDuration = 8;
    public float patienceCalmerDelay = 2;

    [Header("Other")]
    public float victoryDelay = 1;
}

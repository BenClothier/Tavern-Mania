using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    [Header("Orders")]
    public List<Liquid> liquidsAvailable;
    public int maxSingleOrders;
    public int maxDoubleOrders;
    public int maxTripleOrders;

    [Header("Customers")]
    public int customerCount;
    public AnimationCurve customerSpawnPeriodCurve;
    public float patienceMultiplier = 1;
}

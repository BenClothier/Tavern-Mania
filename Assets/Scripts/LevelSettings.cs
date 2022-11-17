using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public List<Liquid> liquidsAvailable;

    [Header("Customers")]
    public int customerCount;
    public AnimationCurve customerSpawnPeriodCurve;
    public float patienceMultiplier = 1;
}

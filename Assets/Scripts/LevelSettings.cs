using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public List<Liquid> liquidsAvailable;
}

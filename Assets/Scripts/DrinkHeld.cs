using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkHeld", menuName = "Game/Character Drink Held")]
public class DrinkHeld : ScriptableObject
{
    private readonly HashSet<Liquid> contents = new();

    public bool IsGlassFull => contents.Count >= 3;

    public bool AddToDrink(Liquid liquid)
    {
        if (!IsGlassFull)
        {
            contents.Add(liquid);
            return true;
        }

        return false;
    }
}

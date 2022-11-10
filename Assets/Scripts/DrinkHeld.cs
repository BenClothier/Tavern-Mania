using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkHeld", menuName = "Game/Character Drink Held")]
public class DrinkHeld : ScriptableObject
{
    public event Action<List<Liquid>> OnDrinkModified;

    private readonly List<Liquid> contents = new();

    public bool IsGlassFull => contents.Count >= 3;

    public bool AddToDrink(Liquid liquid)
    {
        if (!IsGlassFull)
        {
            contents.Add(liquid);
            OnDrinkModified?.Invoke(new List<Liquid>(contents));
            return true;
        }

        return false;
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DrinkHeld", menuName = "Game/Character Drink Held")]
public class DrinkHeld : ScriptableObject
{
    public event Action<DrinkMix> OnDrinkModified;

    public DrinkMix DrinkMix { get; private set; } = DrinkMix.Empty();

    public bool IsGlassFull => DrinkMix.LiquidCount >= 3;

    public bool AddToDrink(Liquid liquid)
    {
        if (!IsGlassFull)
        {
            DrinkMix.AddLiquid(liquid);
            OnDrinkModified?.Invoke(DrinkMix);
            return true;
        }

        return false;
    }

    public void EmptyGlass()
    {
        DrinkMix.Clear();
    }
}

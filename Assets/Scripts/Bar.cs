using System;
using UnityEngine;

public class Bar : MonoBehaviour
{
    public bool IsVacant { get; private set; } = true;

    public DrinkMix? Order { get; private set; }

    public event Action<DrinkMix> OrderSatisfied;

    public event Action<Bar> BarBecameVacant;

    #region Player

    public bool ServeDrink(DrinkMix mix)
    {
        if (Order.HasValue && Order.Value.CompareDrink(mix))
        {
            OrderSatisfied?.Invoke(Order.Value);
            Order = null;
            return true;
        }

        return false;
    }

    #endregion

    #region Customer

    public bool TakeVacancy()
    {
        if (IsVacant == true)
        {
            IsVacant = false;
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void LeaveBar()
    {
        IsVacant = true;
        BarBecameVacant?.Invoke(this);
    }

    public void PlaceOrder(DrinkMix order)
    {
        Order = order;
    }

    #endregion
}

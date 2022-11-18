using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private GameObject orderDisplay1Prefab;
    [SerializeField] private GameObject orderDisplay2Prefab;
    [SerializeField] private GameObject orderDisplay3Prefab;
    [Space]
    [SerializeField] private EventChannel_Void onOrderSatisfied;
    [SerializeField] private EventChannel_Vector2 onOrderFailed;

    public bool IsVacant { get; private set; } = true;

    public DrinkMix? Order { get; private set; }

    public event Action<DrinkMix> ThisOrderSatisfied;

    private GameObject currentOrderDisplay;
    private Queue<Customer> customerQueue = new Queue<Customer>();

    #region Player

    public bool ServeDrink(DrinkMix mix)
    {
        if (Order.HasValue && Order.Value.Equals(mix))
        {
            OnOrderSatisfied();
            return true;
        }

        return false;
    }

    #endregion

    #region Customer

    public bool JoinQueue(Customer customer)
    {
        if (IsVacant)
        {
            customer.InviteToBar(this);
            IsVacant = false;
            return true;
        }
        else
        {
            customerQueue.Enqueue(customer);
            return false;
        }
    }

    public void PlaceOrder(DrinkMix order)
    {
        Debug.Log("Order Placed!");
        Order = order;
        DisplayOrder(order);
    }

    public void FailOrder()
    {
        onOrderFailed.Invoke(transform.position);
        RemoveOrder();
    }

    private void OnOrderSatisfied()
    {
        ThisOrderSatisfied?.Invoke(Order.Value);
        ThisOrderSatisfied = null;

        onOrderSatisfied.Invoke();

        RemoveOrder();
    }

    private void RemoveOrder()
    {
        Order = null;

        IsVacant = true;

        Destroy(currentOrderDisplay);
        currentOrderDisplay = null;

        if (customerQueue.Count > 0)
        {
            Customer customer = customerQueue.Dequeue();
            customer.InviteToBar(this);
            IsVacant = false;
        }
    }

    #endregion

    public void DisplayOrder(DrinkMix order)
    {
        if (order.LiquidCount == 1)
        {
            currentOrderDisplay = Instantiate(orderDisplay1Prefab, transform);
        }
        else if (order.LiquidCount == 2)
        {
            currentOrderDisplay = Instantiate(orderDisplay2Prefab, transform);
        }
        else if (order.LiquidCount == 3)
        {
            currentOrderDisplay = Instantiate(orderDisplay3Prefab, transform);
        }
        else
        {
            Debug.LogError("There were too many or too few liquids to display!");
            return;
        }

        SpriteRenderer[] srs = currentOrderDisplay.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++)
        {
            srs[i].sprite = order.Liquids[i].symbol;
            srs[i].color = order.Liquids[i].colour;
        }
    }
}

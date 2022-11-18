using UnityEngine;

public class MagicalCustomer : Customer
{
    [Header("Magic")]
    [SerializeField] private GameObject patienceCalmerPrefab;

    protected override void OnOrderSatisfied(Bar bar, DrinkMix drinkMix)
    {
        if (CurrentState == CustomerState.Ordered)
        {
            Instantiate(patienceCalmerPrefab, transform.position, Quaternion.identity);
        }

        base.OnOrderSatisfied(bar, drinkMix);
    }
}

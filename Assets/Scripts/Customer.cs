using Pathfinding;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public CustomerState CurrentState { get; private set; }

    private AIPath navigation;

    private void Awake()
    {
        navigation = GetComponent<AIPath>();
    }

    void Start()
    {
        if (FindBar(out Bar bar))
        {
            CurrentState = CustomerState.BarSecured;
            GoToBar(bar);
        }
        else
        {
            CurrentState = CustomerState.Waiting;
            bar.BarBecameVacant += GoToBar;
        }
    }

    private bool FindBar(out Bar bar)
    {
        Bar[] bars = FindObjectsOfType<Bar>();

        var freeBars = bars.Where(bar => bar.IsVacant);

        if (freeBars.Count() < 1)
        {
            bar = bars[(int)(Random.value * bars.Length)];
            return false;
        }
        else
        {
            bar = freeBars.ElementAt((int)(Random.value * freeBars.Count()));
            return true;
        }
    }

    private void GoToBar(Bar bar)
    {
        if (CurrentState == CustomerState.BarSecured)
        {
            navigation.destination = bar.transform.position;
            navigation.SearchPath();
        }
        else
        {
            Debug.LogError($"Performing this action requires states {CustomerState.BarSecured}, the customer was in {CurrentState}!");
        }
    }

    public enum CustomerState
    {
        Waiting,
        BarSecured,
        AtBar,
        Ordered,
        Leaving,
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Customer Event Channel")]
public class EventChannel_Customer : ScriptableObject
{
    public Action<Customer> OnEventInvocation;

    public void Invoke(Customer c)
    {
        OnEventInvocation?.Invoke(c);
    }
}

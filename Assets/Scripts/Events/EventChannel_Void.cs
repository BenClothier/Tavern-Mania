using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Void Event Channel")]
public class EventChannel_Void : ScriptableObject
{
    public Action OnEventInvocation;

    public void Invoke()
    {
        OnEventInvocation?.Invoke();
    }
}

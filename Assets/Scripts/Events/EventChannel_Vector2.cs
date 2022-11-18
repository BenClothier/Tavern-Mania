using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/Vector2 Event Channel")]
public class EventChannel_Vector2 : ScriptableObject
{
    public Action<Vector2> OnEventInvocation;

    public void Invoke(Vector2 vec2)
    {
        OnEventInvocation?.Invoke(vec2);
    }
}

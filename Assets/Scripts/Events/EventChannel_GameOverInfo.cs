using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Events/GameOver Event Channel")]
public class EventChannel_GameOverInfo : ScriptableObject
{
    public Action<GameOverInfo> OnEventInvocation;

    public void Invoke(GameOverInfo info)
    {
        OnEventInvocation?.Invoke(info);
    }
}

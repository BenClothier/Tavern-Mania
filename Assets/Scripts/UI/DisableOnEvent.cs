using UnityEngine;

public class DisableOnEvent : MonoBehaviour
{
    [SerializeField] private EventChannel_Void[] voidEvents;
    [SerializeField] private EventChannel_Vector2[] vectorEvents;
    [SerializeField] private EventChannel_GameOverInfo[] gameOverEvents;

    private void OnEnable()
    {
        foreach (var e in voidEvents)
        {
            e.OnEventInvocation += Disable;
        }

        foreach (var e in vectorEvents)
        {
            e.OnEventInvocation += Disable;
        }

        foreach (var e in gameOverEvents)
        {
            e.OnEventInvocation += Disable;
        }
    }

    private void OnDisable()
    {
        foreach (var e in voidEvents)
        {
            e.OnEventInvocation -= Disable;
        }

        foreach (var e in vectorEvents)
        {
            e.OnEventInvocation -= Disable;
        }

        foreach (var e in gameOverEvents)
        {
            e.OnEventInvocation -= Disable;
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Disable(Vector2 vec)
    {
        gameObject.SetActive(false);
    }

    private void Disable(GameOverInfo info)
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;

public struct GameOverInfo
{
    public Vector2 orderFailedPos;

    public GameOverInfo(Vector2 orderFailedPos)
    {
        this.orderFailedPos = orderFailedPos;
    }
}

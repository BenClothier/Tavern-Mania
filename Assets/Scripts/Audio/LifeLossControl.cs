using UnityEngine;

public class LifeLossControl : MonoBehaviour
{
    public static LifeLossControl instance;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

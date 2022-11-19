using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSmashingControl : MonoBehaviour
{
    public static GlassSmashingControl instance;

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

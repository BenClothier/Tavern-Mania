using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGivingSound : MonoBehaviour
{
    public static DrinkGivingSound instance;

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

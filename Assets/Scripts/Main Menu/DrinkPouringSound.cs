using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkPouringSound : MonoBehaviour
{
    public static DrinkPouringSound instance;
    public AudioSource[] sounds;
    public AudioSource sound1;
    public AudioSource sound2;
    public AudioSource sound3;

    public void Start()
    {
        sounds = GetComponents<AudioSource>();
        sound1 = sounds[0];
        sound2 = sounds[1];
        sound3 = sounds[2];
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl2 : MonoBehaviour
{
    public static MusicControl2 instance;
    public AudioSource[] sounds;
    public AudioSource music1;
    public AudioSource music2;

    public void Start()
    {
        sounds = GetComponents<AudioSource>();
        music1 = sounds[0];
        music2 = sounds[1];
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

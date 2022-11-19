using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl2 : MonoBehaviour
{
    public static MusicControl2 instance;
    public AudioSource[] sounds;
    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;

    public void Start()
    {
        sounds = GetComponents<AudioSource>();
        music1 = sounds[0];
        music2 = sounds[1];
        music3 = sounds[2];
    }

    public void UpdateIntensity(float intensity)
    {
        if ((intensity >= 0) && (intensity < 0.075))
        {
            if (!music1.isPlaying)
            {
                music2.Pause();
                music3.Pause();
                music1.Play();
            }
        } else if ((intensity >= 0.075) && (intensity < 0.15))
        {
            if (!music2.isPlaying)
            {
                music1.Pause();
                music3.Pause();
                music2.Play();
            }
        } else if ((intensity >= 0.15) && (intensity < 0.2))
        {
            if (!music3.isPlaying)
            {
                music1.Pause();
                music2.Pause();
                music3.Play();
            }
        } else if (intensity == 4)
        {
            //play music4
        }
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

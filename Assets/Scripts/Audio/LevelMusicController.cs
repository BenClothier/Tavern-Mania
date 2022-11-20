using UnityEngine;
using UnityEngine.Rendering.UI;

public class LevelMusicController : MonoBehaviour
{
    public static LevelMusicController instance;

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource music3;
    public AudioSource music4;

    public void UpdateIntensity(float intensity)
    {
        if ((intensity >= 0) && (intensity < 0.4f))
        {
            if (music1 != null && !music1.isPlaying)
            {
                PauseAllMusic();
                music1.Play();
            }
            else
            {
                Debug.LogWarning("No Music1 was assigned!");
            }
        } 
        else if ((intensity >= 0.4f) && (intensity < 0.7f))
        {
            if (music2 != null && !music2.isPlaying)
            {
                PauseAllMusic();
                music2.Play();
            }
            else
            {
                Debug.LogWarning("No Music2 was assigned!");
            }
        } 
        else if ((intensity >= 0.7f) && (intensity < 0.9f))
        {
            if (music3 != null && !music3.isPlaying)
            {
                PauseAllMusic();
                music3.Play();
            }
            else
            {
                Debug.LogWarning("No Music3 was assigned!");
            }
        } 
        else if (intensity >= 0.9f)
        {
            if (music4 != null && !music4.isPlaying)
            {
                PauseAllMusic();
                music4.Play();
            }
            else
            {
                Debug.LogWarning("No Music4 was assigned!");
            }
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

    public void PauseAllMusic()
    {
        if (music1 != null)
        {
            music1.Pause();
        }
        if (music2 != null)
        {
            music2.Pause();
        }
        if (music3 != null)
        {
            music3.Pause();
        }
        if (music4 != null)
        {
            music4.Pause();
        }
    }
}

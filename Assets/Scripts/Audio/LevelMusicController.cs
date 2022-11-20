using UnityEngine;
using UnityEngine.Rendering.UI;

public class LevelMusicController : MonoBehaviour
{
    public static LevelMusicController instance;

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource victoryMusic;
    public AudioSource lossMusic;

    private bool playing;

    public void UpdateIntensity(float intensity)
    {
        if (playing == true)
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
        }
    }

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject); 
        if (instance == null)
        {
            instance = this;
            playing = true;
        }
        else 
        {
            Destroy(gameObject); 
        }
    }

    public void StartMusic()
    {
        if (victoryMusic != null)
        {
            victoryMusic.Pause();
        }
        if (lossMusic != null)
        {
            lossMusic.Pause();
        }
        playing = true;
    }

    public void PlayLossMusic()
    {
        lossMusic.Play();
    }

    public void PlayVictoryMusic()
    {
        victoryMusic.Play();
    }

    public void PauseAllMusic()
    {
        playing = false;
        if (music1 != null)
        {
            music1.Pause();
        }
        if (music2 != null)
        {
            music2.Pause();
        }
    }
}

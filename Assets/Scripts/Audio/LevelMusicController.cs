using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class LevelMusicController : MonoBehaviour
{
    [SerializeField] private float volume;
    [SerializeField] private float fadeTime;
    [SerializeField] private EventChannel_GameOverInfo onGameOver;
    [SerializeField] private EventChannel_Void onLevelComplete;

    public AudioSource music1;
    public AudioSource music2;
    public AudioSource victoryMusic;
    public AudioSource lossMusic;

    private LevelController levelController;
    private Coroutine currentMusicFadeRoutine;

    private bool levelEnded;

    private void OnEnable()
    {
        levelController = FindObjectOfType<LevelController>();

        onGameOver.OnEventInvocation += OnGameOver;
        onLevelComplete.OnEventInvocation += OnLevelComplete;

        PauseAllMusic();
        StartCoroutine(PlayLevelMusic());
    }

    public void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
        onLevelComplete.OnEventInvocation -= OnLevelComplete;

        PauseAllMusic();
    }

    private void OnGameOver(GameOverInfo info)
    {
        PauseAllMusic();
        lossMusic.Play();
        levelEnded = true;
    }

    private void OnLevelComplete()
    {
        PauseAllMusic();
        victoryMusic.Play();
        levelEnded = true;
    }

    public void PlayLossMusic()
    {
        Debug.LogError("Still here");
    }

    public void PlayVictoryMusic()
    {
        Debug.LogError("Still here");
    }

    public void PauseAllMusic()
    {
        music1.Pause();
        music2.Pause();
        victoryMusic.Pause();
        lossMusic.Pause();
    }

    private IEnumerator PlayLevelMusic()
    {
        while (enabled && !levelEnded)
        {
            if (!music1.isPlaying && levelController.Intensity < 0.5f)
            {
                if (currentMusicFadeRoutine != null)
                {
                    StopCoroutine(currentMusicFadeRoutine);
                }

                if (music2.isPlaying)
                {
                    currentMusicFadeRoutine = StartCoroutine(FadeBetweenAudioSources(music2, music1, fadeTime));
                }
                else
                {
                    currentMusicFadeRoutine = StartCoroutine(FadeInAudio(music1, fadeTime));
                }
            }
            else if (!music2.isPlaying && levelController.Intensity >= 0.6f)
            {
                if (currentMusicFadeRoutine != null)
                {
                    StopCoroutine(currentMusicFadeRoutine);
                }

                if (music1.isPlaying)
                {
                    currentMusicFadeRoutine = StartCoroutine(FadeBetweenAudioSources(music1, music2, fadeTime));
                }
                else
                {
                    currentMusicFadeRoutine = StartCoroutine(FadeInAudio(music2, fadeTime));
                }
            }

            yield return null;
        }
    }

    private IEnumerator FadeBetweenAudioSources(AudioSource fadeOutAudio, AudioSource fadeInAudio, float fadeTime)
    {
        float timePassed = 0;
        float startInVolume = fadeInAudio.volume;
        float startOutVolume = fadeOutAudio.volume;

        fadeInAudio.time = fadeOutAudio.time;
        fadeInAudio.Play();

        while (timePassed < fadeTime)
        {
            fadeOutAudio.volume = Mathf.Lerp(startOutVolume, 0, timePassed / fadeTime * (startOutVolume / volume));
            fadeInAudio.volume = Mathf.Lerp(startInVolume, volume, timePassed / fadeTime * (1 - startInVolume / volume));
            timePassed += Time.deltaTime;
            yield return null;
        }

        fadeOutAudio.Pause();
    }

    private IEnumerator FadeInAudio(AudioSource fadeInAudio, float fadeTime)
    {
        float timePassed = 0;

        fadeInAudio.time = 0;
        fadeInAudio.volume = 0;
        fadeInAudio.Play();

        while (timePassed < fadeTime)
        {
            fadeInAudio.volume = Mathf.Lerp(0, volume, timePassed / fadeTime);
            timePassed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeOutAudio(AudioSource fadeOutAudio, float fadeTime)
    {
        float timePassed = 0;
        float startOutVolume = fadeOutAudio.volume;

        while (timePassed < fadeTime)
        {
            fadeOutAudio.volume = Mathf.Lerp(startOutVolume, 0, timePassed / fadeTime * (startOutVolume / volume));
            timePassed += Time.deltaTime;
            yield return null;
        }

        fadeOutAudio.Pause();
    }
}

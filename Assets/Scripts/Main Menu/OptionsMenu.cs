using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectVolume", Mathf.Log10(volume) * 20);
    }

    public void GoBack()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

}

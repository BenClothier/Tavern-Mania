using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void Start()
    {
        if (!MenuMusicControl.instance.GetComponent<AudioSource>().isPlaying)
        {
            MenuMusicControl.instance.GetComponent<AudioSource>().Play();
        }
    }

    public void PlayLevelSelect()
    {
        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }

        SceneManager.LoadScene(sceneName: "LevelSelect");
    }

    public void QuitGame()
    {
        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

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
            LevelMusicController.instance.PauseAllMusic();
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
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    }

    public void Options()
    {
        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }

        SceneManager.LoadScene(sceneName: "Options Menu");
    }
}

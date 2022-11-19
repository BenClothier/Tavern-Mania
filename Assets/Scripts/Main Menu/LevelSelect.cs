using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void PlayLevelSelect()
    {
        UISounds.instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(sceneName: "LevelSelect");
    }

    public void QuitGame()
    {
        UISounds.instance.GetComponent<AudioSource>().Play();
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Game is exiting");
    }

    public void Options()
    {
        UISounds.instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(sceneName: "Options Menu");
    }
}

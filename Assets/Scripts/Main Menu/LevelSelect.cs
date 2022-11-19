using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void PlayLevelSelect()
    {
        SceneManager.LoadScene(sceneName: "LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene(sceneName: "Options Menu");
    }
}

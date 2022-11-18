using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayLevel : MonoBehaviour
{

    [SerializeField] public TMP_Text difficulty;

    public void DifficultyIncrease()
    {
        if (difficulty.text == "Easy")
        {
            difficulty.text = "Medium";
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Hard";
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Easy";
        }
    }

    public void DifficultyDecrease()
    {
        if (difficulty.text == "Easy")
        {
            difficulty.text = "Hard";
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Easy";
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Medium";
        }
    }


    public void GoBack()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }

    public void PlayLevel1()
    {
        SceneManager.LoadScene(sceneName: "Level 1");
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(sceneName: "Level 2");
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(sceneName: "Level 3");
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene(sceneName: "Level 4");
    }

    public void PlayLevel5()
    {
        SceneManager.LoadScene(sceneName: "Level 5");
    }

    public void PlayLevel6()
    {
       // SceneManager.LoadScene(sceneName: "Level 6");
    }

    public void PlayLevel7()
    {
        //SceneManager.LoadScene(sceneName: "Level 7");
    }

    public void PlayLevel8()
    {
        //SceneManager.LoadScene(sceneName: "Level 8");
    }

    public void PlayLevel9()
    {
        //SceneManager.LoadScene(sceneName: "Level 9");
    }

    public void PlayLevel10()
    {
        //SceneManager.LoadScene(sceneName: "Level 10");
    }
}

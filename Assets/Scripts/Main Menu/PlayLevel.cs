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
        MusicControl.instance.GetComponent<AudioSource>().Pause();
        MusicControl2.instance.music1.Play();
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(sceneName: "Level 2");
        MusicControl2.instance.music1.Play();
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(sceneName: "Level 3");
        MusicControl2.instance.music1.Play();
    }

    public void PlayLevel4()
    {
        SceneManager.LoadScene(sceneName: "Level 4");
        MusicControl2.instance.music1.Play();
    }

    public void PlayLevel5()
    {
        SceneManager.LoadScene(sceneName: "Level 5");
        MusicControl2.instance.music1.Play();
    }

    public void PlayLevel6()
    {
        // SceneManager.LoadScene(sceneName: "Level 6");
        // MusicControl2.instance.music1.Play();
    }

    public void PlayLevel7()
    {
        //SceneManager.LoadScene(sceneName: "Level 7");
        //MusicControl2.instance.music1.Play();
    }

    public void PlayLevel8()
    {
        //SceneManager.LoadScene(sceneName: "Level 8");
        //MusicControl2.instance.music1.Play();
    }

    public void PlayLevel9()
    {
        //SceneManager.LoadScene(sceneName: "Level 9");
        //MusicControl2.instance.music1.Play();
    }

    public void PlayLevel10()
    {
        //SceneManager.LoadScene(sceneName: "Level 10");
        //MusicControl2.instance.music1.Play();
    }
}

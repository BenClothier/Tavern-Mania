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

        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
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

        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }
    }


    public void GoBack()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");

        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(sceneName: $"Level {level}");
        MenuMusicControl.instance.GetComponent<AudioSource>().Pause();

        if (UISounds.instance)
        {
            UISounds.instance.GetComponent<AudioSource>().Play();
        }

        LevelMusicController.instance.music1.Play();
    }

    public void PlayLevel1()
    {
        LoadLevel(1);
    }

    public void PlayLevel2()
    {
        LoadLevel(2);
    }

    public void PlayLevel3()
    {
        LoadLevel(3);
    }

    public void PlayLevel4()
    {
        LoadLevel(4);
    }

    public void PlayLevel5()
    {
        LoadLevel(5);
    }

    public void PlayLevel6()
    {
        // LoadLevel(6);
    }

    public void PlayLevel7()
    {
        // LoadLevel(7);
    }

    public void PlayLevel8()
    {
        // LoadLevel(8);
    }

    public void PlayLevel9()
    {
        // LoadLevel(9);
    }

    public void PlayLevel10()
    {
        // LoadLevel(10);
    }
}

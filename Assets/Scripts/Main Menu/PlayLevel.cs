using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayLevel : MonoBehaviour
{

    [SerializeField] public TMP_Text difficulty;
    [SerializeField] public IntVariable livesPerLevel;

    private void OnEnable()
    {
        switch (livesPerLevel.Value)
        {
            case 1:
                difficulty.text = "Hard";
                break;
            case 2:
                difficulty.text = "Medium";
                break;
            case 3:
                difficulty.text = "Easy";
                break;
        }
    }

    public void DifficultyIncrease()
    {
        if (difficulty.text == "Easy")
        {
            difficulty.text = "Medium";
            livesPerLevel.Value = 2;
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Hard";
            livesPerLevel.Value = 1;
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Easy";
            livesPerLevel.Value = 3;
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
            livesPerLevel.Value = 1;
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Easy";
            livesPerLevel.Value = 3;
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Medium";
            livesPerLevel.Value = 2;
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

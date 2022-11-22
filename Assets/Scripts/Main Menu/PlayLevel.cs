using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public static List<int> levelsCompleted = new List<int>();

    [SerializeField] public TMP_Text difficulty;
    [SerializeField] public TMP_Text[] levelButtons;
    [SerializeField] private Color32 levelCompleteColor = new Color32(24, 255, 140, 255);

    private void OnEnable()
    {
        switch (LevelController.LivesPerLevel)
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

        for (int i = 0; i < levelButtons.Length; i++)
        {
            TMP_Text text = levelButtons[i];

            if (levelsCompleted.Contains(i + 1))
            {
                text.color = levelCompleteColor;
            }
            else
            {
                text.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void DifficultyIncrease()
    {
        if (difficulty.text == "Easy")
        {
            difficulty.text = "Medium";
            LevelController.LivesPerLevel = 2;
            LevelController.SpawnPeriodMultiplier = 1f;
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Hard";
            LevelController.LivesPerLevel = 1;
            LevelController.SpawnPeriodMultiplier = 0.85f;
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Easy";
            LevelController.LivesPerLevel = 3;
            LevelController.SpawnPeriodMultiplier = 1.2f;
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
            LevelController.LivesPerLevel = 1;
            LevelController.SpawnPeriodMultiplier = 0.9f;
        }
        else if (difficulty.text == "Medium")
        {
            difficulty.text = "Easy";
            LevelController.LivesPerLevel = 3;
            LevelController.SpawnPeriodMultiplier = 1.15f;
        }
        else if (difficulty.text == "Hard")
        {
            difficulty.text = "Medium";
            LevelController.LivesPerLevel = 2;
            LevelController.SpawnPeriodMultiplier = 1f;
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
        LoadLevel(6);
    }

    public void PlayLevel7()
    {
        LoadLevel(7);
    }

    public void PlayLevel8()
    {
        LoadLevel(8);
    }

    public void PlayLevel9()
    {
        LoadLevel(9);
    }

    public void PlayLevel10()
    {
        LoadLevel(10);
    }
}

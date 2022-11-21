using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayLevel : MonoBehaviour
{

    [SerializeField] public TMP_Text difficulty;
    [SerializeField] public IntVariable livesPerLevel;

    [SerializeField] public TMP_Text level1Text;
    [SerializeField] public TMP_Text level2Text;
    [SerializeField] public TMP_Text level3Text;
    [SerializeField] public TMP_Text level4Text;
    [SerializeField] public TMP_Text level5Text;
    [SerializeField] public TMP_Text level6Text;
    [SerializeField] public TMP_Text level7Text;
    [SerializeField] public TMP_Text level8Text;
    [SerializeField] public TMP_Text level9Text;
    [SerializeField] public TMP_Text level10Text;

    public static bool level1Complete { get; private set; }
    public static bool level2Complete { get; private set; }
    public static bool level3Complete { get; private set; }
    public static bool level4Complete { get; private set; }
    public static bool level5Complete { get; private set; }
    public static bool level6Complete { get; private set; }
    public static bool level7Complete { get; private set; }
    public static bool level8Complete { get; private set; }
    public static bool level9Complete { get; private set; }
    public static bool level10Complete { get; private set; }


    private LevelController levelController;

    public void Update()
    {
        if (level1Complete == true)
        {
            level1Text.color = new Color32(255, 255, 0, 255);
        } else if (level1Complete == false)
        {
            level1Text.color = new Color32(255, 255, 255, 255);
        }
        if (level2Complete == true)
        {
            level2Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level2Complete == false)
        {
            level2Text.color = new Color32(255, 255, 255, 255);
        }
        if (level3Complete == true)
        {
            level3Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level3Complete == false)
        {
            level3Text.color = new Color32(255, 255, 255, 255);
        }
        if (level4Complete == true)
        {
            level4Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level4Complete == false)
        {
            level4Text.color = new Color32(255, 255, 255, 255);
        }
        if (level5Complete == true)
        {
            level5Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level5Complete == false)
        {
            level5Text.color = new Color32(255, 255, 255, 255);
        }
        if (level6Complete == true)
        {
            level6Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level6Complete == false)
        {
            level6Text.color = new Color32(255, 255, 255, 255);
        }
        if (level7Complete == true)
        {
            level7Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level7Complete == false)
        {
            level7Text.color = new Color32(255, 255, 255, 255);
        }
        if (level8Complete == true)
        {
            level8Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level8Complete == false)
        {
            level8Text.color = new Color32(255, 255, 255, 255);
        }
        if (level9Complete == true)
        {
            level9Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level9Complete == false)
        {
            level9Text.color = new Color32(255, 255, 255, 255);
        }
        if (level10Complete == true)
        {
            level10Text.color = new Color32(255, 255, 0, 255);
        }
        else if (level10Complete == false)
        {
            level10Text.color = new Color32(255, 255, 255, 255);
        }
    }

    public static void levelComplete(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                level1Complete = true;
                break;
            case 2:
                level2Complete = true;
                break;
            case 3:
                level3Complete = true;
                break;
            case 4:
                level4Complete = true;
                break;
            case 5:
                level5Complete = true;
                break;
            case 6:
                level6Complete = true;
                break;
            case 7:
                level7Complete = true;
                break;
            case 8:
                level8Complete = true;
                break;
            case 9:
                level9Complete = true;
                break;
            case 10:
                level10Complete = true;
                break;
        }
    }

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

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private EventChannel_Void onLevelComplete;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject leaveButton;

    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
        image.enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        onLevelComplete.OnEventInvocation += OnLevelComplete;
    }

    private void OnDisable()
    {
        onLevelComplete.OnEventInvocation -= OnLevelComplete;
    }

    private void OnLevelComplete()
    {
        image.enabled = true;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            continueButton.SetActive(false);
            var leaveRT = leaveButton.GetComponent<RectTransform>();
            leaveRT.anchoredPosition = new Vector2(0.5f, leaveRT.anchoredPosition.y);
        }
    }

    public void ContinuePressed()
    {
        var op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        op.completed += _ => Time.timeScale = 1;
    }

    public void LeavePressed()
    {
        var op = SceneManager.LoadSceneAsync("Main Menu");
        op.completed += _ => Time.timeScale = 1;
    }
}

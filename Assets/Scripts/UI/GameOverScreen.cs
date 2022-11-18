using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private EventChannel_GameOverInfo onGameOver;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        onGameOver.OnEventInvocation += OnGameOver;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
    }

    private void OnGameOver(GameOverInfo info)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void RetryPressed()
    {
        var op = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        op.completed += _ => Time.timeScale = 1;
    }

    public void LeavePressed()
    {
        var op = SceneManager.LoadSceneAsync("Main Menu");
        op.completed += _ => Time.timeScale = 1;
    }
}

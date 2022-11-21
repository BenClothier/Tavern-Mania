using UnityEngine;

public class HeartBoxController : MonoBehaviour
{
    [SerializeField] private IntVariable livesRemaining;
    [SerializeField] private GameObject[] hearts;

    private void Start()
    {
        OnLivesChanged(livesRemaining.Value);
        livesRemaining.OnVariableModified += OnLivesChanged;
    }

    private void OnDisable()
    {
        livesRemaining.OnVariableModified -= OnLivesChanged;
    }

    private void OnLivesChanged(int lives)
    {
        Debug.Log("Updating Hearts: " + lives);

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < lives);
        }
    }
}

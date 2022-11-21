using UnityEngine;

public class HeartBoxController : MonoBehaviour
{
    [SerializeField] private IntVariable livesRemaining;

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
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i < livesRemaining.Value);
        }
    }
}

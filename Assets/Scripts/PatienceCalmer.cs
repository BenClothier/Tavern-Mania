using UnityEngine;

public class PatienceCalmer : MonoBehaviour
{
    [SerializeField] private FloatVariable patienceDropMultiplier;

    private LevelSettings lvlSettings;

    private void OnEnable()
    {
        lvlSettings = FindObjectOfType<LevelController>().LevelSettings;
        patienceDropMultiplier.Value = lvlSettings.patienceCalmerPatienceDropMultiplier;
        Destroy(gameObject, lvlSettings.patienceCalmerDuration);
    }

    private void OnDisable()
    {
        Debug.Log("Calmers:" + FindObjectsOfType<PatienceCalmer>().Length);
        if (FindObjectsOfType<PatienceCalmer>().Length <= 1)
        {
            patienceDropMultiplier.Value = lvlSettings.patienceDropMultiplier;
        }
    }
}

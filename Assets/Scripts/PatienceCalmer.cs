using System.Collections;
using UnityEngine;

public class PatienceCalmer : MonoBehaviour
{
    [SerializeField] private FloatVariable patienceDropMultiplier;

    private LevelSettings lvlSettings;

    private void OnEnable()
    {
        lvlSettings = FindObjectOfType<LevelController>().LevelSettings;
        StartCoroutine(ActivateAfterDelay());
    }

    private void OnDisable()
    {
        if (FindObjectsOfType<PatienceCalmer>().Length <= 1)
        {
            patienceDropMultiplier.Value = lvlSettings.patienceDropMultiplier;
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(lvlSettings.patienceCalmerDelay);
        patienceDropMultiplier.Value = lvlSettings.patienceCalmerPatienceDropMultiplier;
        Destroy(gameObject, lvlSettings.patienceCalmerDuration);
    }
}

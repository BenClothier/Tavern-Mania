using UnityEngine;
using UnityEngine.Assertions;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelSettings levelSettings;

    private void Awake()
    {
        Barrel[] barrels = FindObjectsOfType<Barrel>();
        Assert.IsTrue(barrels.Length == levelSettings.liquidsAvailable.Count, "Barrel count does not match liquid count!!");

        for (int i = 0; i < barrels.Length; i++)
        {
            barrels[i].liquid = levelSettings.liquidsAvailable[i];
        }
    }
}

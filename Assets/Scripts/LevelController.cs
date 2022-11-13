using System.Collections.Generic;
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
            barrels[i].Liquid = levelSettings.liquidsAvailable[i];
        }
    }

    public DrinkMix GenerateOrder(Customer customer)
    {
        List<Liquid> liquids = new ();
        int liquidCount = UnityEngine.Random.Range(1, 4);

        for (int i = 0; i < liquidCount; i++)
        {
            liquids.Add(levelSettings.liquidsAvailable[UnityEngine.Random.Range(0, levelSettings.liquidsAvailable.Count)]);
        }

        return new DrinkMix(liquids);
    }
}

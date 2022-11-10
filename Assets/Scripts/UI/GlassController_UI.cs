using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GlassController_UI : MonoBehaviour
{
    [SerializeField] private DrinkHeld characterDrink;
    [SerializeField] private Image glassFillImage;

    private void Awake()
    {
        glassFillImage.fillAmount = 0;
        glassFillImage.color = Color.clear;
        characterDrink.OnDrinkModified += OnDrinkModified;
    }

    private void OnDrinkModified(List<Liquid> liquids)
    {
        Debug.Log("Updating glass UI.");

        glassFillImage.fillAmount = (float)liquids.Count / 3;
        glassFillImage.color = liquids.Aggregate(Color.black, (col, liq) => col + liq.colour) / liquids.Count;
    }
}

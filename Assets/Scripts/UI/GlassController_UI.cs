using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GlassController_UI : MonoBehaviour
{
    [SerializeField] private DrinkHeld characterDrink;
    [SerializeField] private Image glassFillImage;
    [SerializeField] private float animationSpeed;

    private float targetFillAmount;
    private Color targetColour;

    private void Awake()
    {
        glassFillImage.fillAmount = 0;
        glassFillImage.color = Color.clear;
        characterDrink.OnDrinkModified += OnDrinkModified;
    }

    private void OnDrinkModified(List<Liquid> liquids)
    {
        Debug.Log("Updating glass UI.");

        targetFillAmount = (float)liquids.Count / 3;
        targetColour = liquids.Aggregate(Color.black, (col, liq) => col + liq.colour) / liquids.Count;

        // If first liquid to be added, just take it's colour
        if (liquids.Count == 1)
        {
            glassFillImage.color = liquids[0].colour;
        }
    }

    private void Update()
    {
        glassFillImage.fillAmount = Mathf.Lerp(glassFillImage.fillAmount, targetFillAmount, animationSpeed * Time.deltaTime);
        glassFillImage.color = Color.Lerp(glassFillImage.color, targetColour, animationSpeed * Time.deltaTime);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GlassController_UI : MonoBehaviour
{
    [SerializeField] private DrinkHeld characterDrink;
    [SerializeField] private Image glassFillImage;
    [SerializeField] private float animationSpeed;

    [SerializeField] private EventChannel_GameOverInfo onGameOver;

    private float targetFillAmount;
    private Color targetColour;

    private void OnEnable()
    {
        glassFillImage.fillAmount = 0;
        glassFillImage.color = Color.clear;
        characterDrink.OnDrinkModified += OnDrinkModified;
        onGameOver.OnEventInvocation += OnGameOver;
    }

    private void OnDisable()
    {
        characterDrink.OnDrinkModified -= OnDrinkModified;
        onGameOver.OnEventInvocation -= OnGameOver;
    }

    private void OnDrinkModified(DrinkMix mix)
    {
        Debug.Log("Updating glass UI.");

        targetFillAmount = (float)mix.LiquidCount / 3;
        targetColour = mix.Liquids.Aggregate(Color.black, (col, liq) => col + liq.colour) / mix.LiquidCount;

        // If first liquid to be added, just take it's colour
        if (mix.LiquidCount == 1)
        {
            glassFillImage.color = mix.Liquids[0].colour;
        }
    }

    private void OnGameOver(GameOverInfo info)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        glassFillImage.fillAmount = Mathf.Lerp(glassFillImage.fillAmount, targetFillAmount, animationSpeed * Time.deltaTime);
        glassFillImage.color = Color.Lerp(glassFillImage.color, targetColour, animationSpeed * Time.deltaTime);
    }
}

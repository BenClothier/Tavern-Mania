using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering;

public class GlassController_UI : MonoBehaviour
{
    [SerializeField] private DrinkHeld characterDrink;
    [SerializeField] private Image glassFillImage;
    [SerializeField] private float animationSpeed;

    [SerializeField] private EventChannel_GameOverInfo onGameOver;
    [SerializeField] private EventChannel_GameOverInfo onLevelComplete;

    private float targetFillAmount;
    private Color targetColour;

    private Animator animator;
    private DrinkMix newMix;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        glassFillImage.fillAmount = 0;
        glassFillImage.color = Color.clear;
        characterDrink.OnDrinkModified += OnDrinkModified;
        onGameOver.OnEventInvocation += Disable;
        onLevelComplete.OnEventInvocation += Disable;
    }

    private void OnDisable()
    {
        characterDrink.OnDrinkModified -= OnDrinkModified;
        onGameOver.OnEventInvocation -= Disable;
        onLevelComplete.OnEventInvocation -= Disable;
    }

    private void OnDrinkModified(DrinkMix mix)
    {
        newMix = mix;

        if (mix.LiquidCount < 1)
        {
            animator.SetTrigger("Empty");
        }
        else
        {
            UpdateDrink();
            animator.SetTrigger("Fill");
        }
    }

    public void UpdateDrink()
    {
        targetFillAmount = (float)newMix.LiquidCount / 3;
        targetColour = newMix.Liquids.Aggregate(Color.black, (col, liq) => col + liq.colour) / newMix.LiquidCount;

        // If first liquid to be added, just take it's colour
        if (newMix.LiquidCount == 1)
        {
            glassFillImage.color = newMix.Liquids[0].colour;
        }
    }

    private void Disable(GameOverInfo info)
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        glassFillImage.fillAmount = Mathf.Lerp(glassFillImage.fillAmount, targetFillAmount, animationSpeed * Time.deltaTime);
        glassFillImage.color = Color.Lerp(glassFillImage.color, targetColour, animationSpeed * Time.deltaTime);
    }
}

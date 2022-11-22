using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float movementSpeed = 1;

    [Header("Pouring Drink / Serving")]
    [SerializeField] private float pourDrinkTime = 1;
    [SerializeField] private DrinkHeld drinkHeld;

    [Header("Effects")]
    [SerializeField] private ParticleSystem throwGlassEffect;
    [SerializeField] private GameObject progressBarGroup;
    [SerializeField] private SpriteRenderer progressBarFill;

    private Rigidbody2D rb;
    private Animator animator;

    private GridController gridController;

    private bool pouringDrink;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gridController = FindObjectOfType<GridController>();
        progressBarGroup.SetActive(false);
    }

    private void Update()
    {
        Vector2 movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 clampedMovement = Vector2.ClampMagnitude(movementVector, 1);
        Vector2 normMovement;

        if (movementVector.y != 0)
        {
            normMovement = Vector2.up * Mathf.Sign(movementVector.y);
        }
        else if (movementVector.x != 0)
        {
            normMovement = Vector2.right * Mathf.Sign(movementVector.x);
        }
        else
        {
            normMovement = Vector2.zero;
        }

        if (!pouringDrink)
        {
            rb.velocity = clampedMovement * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        animator.SetFloat("Horizontal", clampedMovement.x);
        animator.SetFloat("Vertical", clampedMovement.y);

        Vector2 highlightPos = (Vector2)transform.position + normMovement;
        gridController.HighlightTile(highlightPos);

        Debug.DrawRay(transform.position, normMovement, Color.green);

        if (Input.GetMouseButtonDown(0) && gridController.GetTileGameObject(highlightPos, out GameObject tileObject) && tileObject.TryGetComponent(out Bar bar))
        {
            if (bar.ServeDrink(drinkHeld.DrinkMix))
            {
                drinkHeld.EmptyGlass();
                DrinkGivingSound.instance.GetComponent<AudioSource>().Play(); //Play Drink Giving Sound
            }
            else
            {
                Debug.LogWarning("Could not serve held drink.");
            }
        }
        else if (!pouringDrink && !drinkHeld.DrinkMix.IsFull && Input.GetMouseButton(0) && gridController.GetTileGameObject(highlightPos, out GameObject tile) && tile.TryGetComponent(out Barrel barrel))
        {
            StartCoroutine(PourDrinkRoutine(barrel));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            drinkHeld.EmptyGlass();
            throwGlassEffect.Play();
            GlassSmashingControl.instance.GetComponent<AudioSource>().Play(); //Play Glass Smashing Sound
        }
    }

    private IEnumerator PourDrinkRoutine(Barrel barrel)
    {
        pouringDrink = true;
        progressBarGroup.SetActive(true);
        progressBarFill.color = barrel.Liquid.colour;

        // Sound effect
        int initialLiquidCount = drinkHeld.DrinkMix.LiquidCount;

        switch (initialLiquidCount)
        {
            case 0:
                DrinkPouringSound.instance.sound1.Play();
                break;
            case 1:
                DrinkPouringSound.instance.sound2.Play();
                break;
            case 2:
                DrinkPouringSound.instance.sound3.Play();
                break;
        }

        // Routine
        float timeLeft = pourDrinkTime;

        while (Input.GetMouseButton(0) && timeLeft > 0)
        {
            yield return null;
            timeLeft -= Time.deltaTime;
            float fillAmount = 1 - (timeLeft / pourDrinkTime);
            progressBarFill.transform.localPosition = new Vector2(fillAmount * .5f - .5f, progressBarFill.transform.localPosition.y);
            progressBarFill.size = new Vector2(fillAmount, progressBarFill.size.y);
        }

        progressBarGroup.SetActive(false);
        pouringDrink = false;

        // Success or cancelled
        if (timeLeft <= 0)
        {
            // Take liquid
            if (barrel.TakeMeasurementOfContents(out Liquid liquid))
            {
                if (drinkHeld.AddToDrink(liquid))
                {
                    Debug.Log($"{liquid.Name} added to glass.");
                }
                else
                {
                    Debug.LogWarning("Could not add liquid to glass.");
                }
            }
            else
            {
                Debug.LogWarning("Could not take barrel contents.");
            }
        }
        else
        {
            // Cut off sound
            switch (initialLiquidCount)
            {
                case 0:
                    if (DrinkPouringSound.instance.sound1.isPlaying)
                    {
                        DrinkPouringSound.instance.sound1.Pause();
                    }
                    break;
                case 1:
                    if (DrinkPouringSound.instance.sound2.isPlaying)
                    {
                        DrinkPouringSound.instance.sound2.Pause();
                    }
                    break;
                case 2:
                    if (DrinkPouringSound.instance.sound3.isPlaying)
                    {
                        DrinkPouringSound.instance.sound3.Pause();
                    }
                    break;
            }
        }
    }
}

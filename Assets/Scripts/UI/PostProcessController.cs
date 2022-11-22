using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [Header("Game Over Effects")]
    [SerializeField] private EventChannel_GameOverInfo onGameOver;
    [SerializeField] private float vignetteGameOverIntensity = 0.3f;
    [SerializeField] private float vignetteGameOverLerpSpeed = 1.5f;

    [Header("Magic Effects")]
    [SerializeField] private EventChannel_Void onMagicStart;
    [SerializeField] private EventChannel_Void onMagicEnd;
    [SerializeField] private Color vignetteMagicColour;
    [SerializeField] private float vignetteMagicIntensity = 0.3f;
    [SerializeField] private float vignetteMagicLerpSpeed = 1.5f;

    [Header("Life Loss Effect")]
    [SerializeField] private EventChannel_Vector2 onOrderFailed;
    [SerializeField] private Color vignetteFailColour;
    [SerializeField] private float vignetteLifeLossIntensity = 0.3f;
    [SerializeField] private float vignetteLifeLossDuration = 1.5f;

    private Volume volume;
    private Vignette vignette;

    private Color defaultVignetteColour;
    private float defaultVignetteIntensity;
    private bool magicOn;

    private Coroutine gameOverRoutine;
    private Coroutine magicRoutine;
    private Coroutine lifeLossRoutine;

    private LevelController levelController;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        levelController = FindObjectOfType<LevelController>();

        defaultVignetteColour = vignette.color.value;
        defaultVignetteIntensity = vignette.intensity.value;

        onGameOver.OnEventInvocation += OnGameOver;
        onMagicStart.OnEventInvocation += OnMagicStart;
        onMagicEnd.OnEventInvocation += OnMagicEnd;
        onOrderFailed.OnEventInvocation += OnLifeLoss;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
        onMagicStart.OnEventInvocation -= OnMagicStart;
        onMagicEnd.OnEventInvocation -= OnMagicEnd;
        onOrderFailed.OnEventInvocation -= OnLifeLoss;
    }

    private void OnGameOver(GameOverInfo info)
    {
        StopAllCoroutines();
        StartCoroutine(LerpGameOverVignette());
    }

    private IEnumerator LerpGameOverVignette()
    {
        while (enabled)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.GetValue<float>(), vignetteGameOverIntensity, vignetteGameOverLerpSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        gameOverRoutine = null;
    }

    private void OnMagicStart()
    {
        if (gameOverRoutine == null)
        {
            StopAllCoroutines();
            magicOn = true;
            magicRoutine = StartCoroutine(LerpMagicVignette());
        }
    }

    private void OnMagicEnd()
    {
        magicOn = false;
    }

    private IEnumerator LerpMagicVignette()
    {
        while (enabled && !(!magicOn && vignette.intensity.value <= defaultVignetteIntensity + 0.001f))
        {
            if (magicOn)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.GetValue<float>(), vignetteMagicIntensity, vignetteMagicLerpSpeed * Time.unscaledDeltaTime);
                vignette.color.value = Color.Lerp(vignette.color.GetValue<Color>(), vignetteMagicColour, vignetteMagicLerpSpeed * Time.unscaledDeltaTime);
            }
            else
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.GetValue<float>(), defaultVignetteIntensity, vignetteMagicLerpSpeed * Time.unscaledDeltaTime);
                vignette.color.value = Color.Lerp(vignette.color.GetValue<Color>(), defaultVignetteColour, vignetteMagicLerpSpeed * Time.unscaledDeltaTime);
            }

            yield return null;
        }

        magicRoutine = null;
    }

    private void OnLifeLoss(Vector2 vec2)
    {
        if (!levelController.GameOver && gameOverRoutine == null && magicRoutine == null)
        {
            StopAllCoroutines();
            lifeLossRoutine = StartCoroutine(LerpLifeLossVignette());
        }
    }

    private IEnumerator LerpLifeLossVignette()
    {
        float timePassed = 0;

        while (enabled && timePassed <= vignetteLifeLossDuration)
        {
            if (timePassed / vignetteLifeLossDuration < 0.5f)
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.GetValue<float>(), vignetteLifeLossIntensity, timePassed / (vignetteLifeLossDuration / 2));
                vignette.color.value = Color.Lerp(vignette.color.GetValue<Color>(), vignetteFailColour, timePassed / (vignetteLifeLossDuration / 2));
            }
            else
            {
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.GetValue<float>(), defaultVignetteIntensity, timePassed / (vignetteLifeLossDuration / 2) - 1);
                vignette.color.value = Color.Lerp(vignette.color.GetValue<Color>(), defaultVignetteColour, timePassed / (vignetteLifeLossDuration / 2) - 1);
            }

            timePassed += Time.deltaTime;

            yield return null;
        }

        lifeLossRoutine = null;
    }
}

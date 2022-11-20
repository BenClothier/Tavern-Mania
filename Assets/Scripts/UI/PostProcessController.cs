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

    private Volume volume;
    private Vignette vignette;

    private Color defaultVignetteColour;
    private float defaultVignetteIntensity;
    private bool magicOn;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        defaultVignetteColour = vignette.color.value;
        defaultVignetteIntensity = vignette.intensity.value;

        onGameOver.OnEventInvocation += OnGameOver;
        onMagicStart.OnEventInvocation += OnMagicStart;
        onMagicEnd.OnEventInvocation += OnMagicEnd;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
        onMagicStart.OnEventInvocation -= OnMagicStart;
        onMagicEnd.OnEventInvocation -= OnMagicEnd;
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
    }

    private void OnMagicStart()
    {
        magicOn = true;
        StartCoroutine(LerpMagicVignette());
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
    }
}

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

    private Volume volume;
    private Vignette vignette;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet(out vignette);

        onGameOver.OnEventInvocation += OnGameOver;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
    }

    private void OnGameOver(GameOverInfo info)
    {
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
}

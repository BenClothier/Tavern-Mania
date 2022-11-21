using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private float lerpSpeed = 1;
    [SerializeField] private float cameraSizeOneGameOver;
    [SerializeField] private EventChannel_GameOverInfo onGameOver;

    [Header("Shake")]
    [SerializeField] private AnimationCurve frequencyByIntesnity;
    [SerializeField] private AnimationCurve amplitudeByIntesnity;
    [SerializeField] private Vector2 shakeScale;
    [SerializeField] private Vector2 shakePhaseOffset;

    private LevelController levelController;
    private float timePassed = 0;
    private Vector3 originalPosition;
    private bool stopShake;

    private void OnEnable()
    {
        levelController = FindObjectOfType<LevelController>();
        originalPosition = transform.position;
        onGameOver.OnEventInvocation += OnGameOver;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
        StopAllCoroutines();
    }

    private void Update()
    {
        ComputeShake();
    }

    private void OnGameOver(GameOverInfo info)
    {
        stopShake = true;
        StartCoroutine(MoveCameraRoutine(info));
    }

    private IEnumerator MoveCameraRoutine(GameOverInfo info)
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        while (levelController != null && levelController.GameOver)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(info.orderFailedPos.x, info.orderFailedPos.y, transform.position.z), lerpSpeed * Time.unscaledDeltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, cameraSizeOneGameOver, lerpSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
    }

    private void ComputeShake()
    {
        if (!stopShake)
        {
            float frequency = frequencyByIntesnity.Evaluate(levelController.Intensity);
            float amplitude = amplitudeByIntesnity.Evaluate(levelController.Intensity);

            transform.position = originalPosition + new Vector3(Mathf.Sin(timePassed + shakePhaseOffset.x) * shakeScale.x, Mathf.Sin(timePassed + shakePhaseOffset.y) * shakeScale.y, 0) * amplitude;

            timePassed += Time.unscaledDeltaTime * frequency;
        }
    }
}

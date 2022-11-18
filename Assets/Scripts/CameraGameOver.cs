using System.Collections;
using UnityEngine;

public class CameraGameOver : MonoBehaviour
{
    [SerializeField] private float lerpSpeed = 1;
    [SerializeField] private float cameraSizeOneGameOver;
    [SerializeField] private EventChannel_GameOverInfo onGameOver;

    private void OnEnable()
    {
        onGameOver.OnEventInvocation += OnGameOver;
    }

    private void OnDisable()
    {
        onGameOver.OnEventInvocation -= OnGameOver;
        StopAllCoroutines();
    }

    private void OnGameOver(GameOverInfo info)
    {
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
}

using System.Collections;
using UnityEngine;

namespace TaskbarBreakout.Core
{
    public class CameraAdapter : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        [SerializeField] private float transitionDuration = 0.3f;

        private Coroutine _transition;

        private void Awake()
        {
            if (targetCamera == null)
                targetCamera = Camera.main;
        }

        // Sprite PPU = 100 (Unity default). OrthoSize = yarı yükseklik / PPU
        private const float PixelsPerUnit = 100f;

        public void AdaptTo(Rect gameBounds)
        {
            float orthoSize = gameBounds.height * 0.5f / PixelsPerUnit;

            if (_transition != null)
                StopCoroutine(_transition);
            _transition = StartCoroutine(Transition(orthoSize));
        }

        private IEnumerator Transition(float targetOrtho)
        {
            float startOrtho = targetCamera.orthographicSize;
            float elapsed = 0f;

            while (elapsed < transitionDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / transitionDuration);
                targetCamera.orthographicSize = Mathf.Lerp(startOrtho, targetOrtho, t);
                yield return null;
            }

            targetCamera.orthographicSize = targetOrtho;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.GameUI.Faders
{
    [RequireComponent(typeof(Image))]
    public class ImageFader : MonoBehaviour
    {
        /// <summary>
        /// The curve to fade the image in with.
        /// </summary>
        public AnimationCurve fadeCurve;

        /// <summary>
        /// The image to fade in.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The fade duration at which the image fades in.
        /// </summary>
        private float _fadeDuration = 2.5f;

        /// <summary>
        /// The target alpha value for the image.
        /// </summary>
        private float _targetAlpha = 0.51f;

        /// <summary>
        /// Called before the first frame update.
        ///   - Sets the image to fade in.
        ///   - Starts the fade in coroutine.
        /// </summary>
        private void OnEnable()
        {
            _image = GetComponent<Image>();
            if (_image != null)
            {
                StartCoroutine(FadeIn());
            }
        }

        /// <summary>
        /// Fades in the alpha value of the image to the target alpha value smoothly over a specified duration.
        /// </summary>
        /// <returns>An enumerator that performs the fading operation over time.</returns>
        private IEnumerator FadeIn()
        {
            Color currentColor = _image.color;
            float startTime = Time.time;

            while (Time.time - startTime < _fadeDuration)
            {
                float timeElapsed = Time.time - startTime;
                float t = timeElapsed / _fadeDuration;
                float alpha = _targetAlpha * fadeCurve.Evaluate(t);
                currentColor.a = alpha;
                _image.color = currentColor;
                yield return null;
            }

            currentColor.a = _targetAlpha;
            _image.color = currentColor;
        }
    }
}
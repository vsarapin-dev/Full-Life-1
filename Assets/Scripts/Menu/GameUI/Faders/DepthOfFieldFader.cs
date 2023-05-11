using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Menu.GameUI.Faders
{
    public class DepthOfFieldFader : MonoBehaviour
    {
        /// <summary>
        /// Global volume.
        /// </summary>
        private Volume _globalVolume;

        /// <summary>
        /// Focal length.
        /// </summary>
        private ClampedFloatParameter _focalLength;

        /// <summary>
        /// Aperture shape.
        /// </summary>
        private ClampedFloatParameter _aperture;

        /// <summary>
        /// Blade count.
        /// </summary>
        private ClampedIntParameter _bladeCount;

        /// <summary>
        /// Volume profile.
        /// </summary>
        private VolumeProfile _volumeProfile;

        /// <summary>
        /// The fade duration at which the volume blur fades in.
        /// </summary>
        private float _fadeDuration = 2.5f;

        /// <summary>
        /// The target focal length value for the DepthOfField.
        /// </summary>
        private float _targetFocalLength = 100f;

        /// <summary>
        /// The target aperture shape value for the DepthOfField.
        /// </summary>
        private float _targetApertureShape = 32f;

        /// <summary>
        /// The target blade count value for the DepthOfField.
        /// </summary>
        private int _targetBladeCount = 5;


        /// <summary>
        /// Called before the first frame update.
        /// </summary>
        private void OnEnable()
        {
            Volume _globalVolume = FindObjectOfType<Volume>();
            _volumeProfile = _globalVolume.profile;

            if (_volumeProfile == null) return;

            DepthOfField dof;
            if (_volumeProfile.TryGet(out dof))
            {
                _focalLength = dof.focalLength;
                _aperture = dof.aperture;
                _bladeCount = dof.bladeCount;

                StartCoroutine(FadeIn());
            }
        }

        /// <summary>
        /// Changes the values of _focalLength, _aperture, and _bladeCount parameters to their target values smoothly over a certain amount of time.
        /// </summary>
        /// <returns>An enumerator that performs the lerping operation over time.</returns>
        private IEnumerator FadeIn()
        {
            float timeElapsed = 0f;

            AnimationCurve focalLengthCurve = AnimationCurve.EaseInOut(0f, _focalLength.value, _fadeDuration, _targetFocalLength);
            AnimationCurve apertureCurve = AnimationCurve.EaseInOut(0f, _aperture.value, _fadeDuration, _targetApertureShape);
            AnimationCurve bladeCountCurve = AnimationCurve.EaseInOut(0f, _bladeCount.value, _fadeDuration, _targetBladeCount);

            while (timeElapsed < _fadeDuration)
            {
                float t = timeElapsed / _fadeDuration;
                _focalLength.value = focalLengthCurve.Evaluate(t);
                _aperture.value = apertureCurve.Evaluate(t);
                _bladeCount.value = Mathf.RoundToInt(bladeCountCurve.Evaluate(t));

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _focalLength.value = _targetFocalLength;
            _aperture.value = _targetApertureShape;
            _bladeCount.value = _targetBladeCount;
        }
    }
}
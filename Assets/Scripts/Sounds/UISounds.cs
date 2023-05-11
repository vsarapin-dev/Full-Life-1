using UnityEngine;

namespace Sounds
{
    /// <summary>
    /// This script is used to play sounds for the UI.
    /// </summary>
    public class UISounds : MonoBehaviour
    {
        /// <summary>
        /// The hover sound.
        /// </summary>
        [SerializeField] private AudioSource hoverSound;

        /// <summary>
        /// The click sound.
        /// </summary>
        [SerializeField] private AudioSource clickSound;

        /// <summary>
        /// Makes the object not be destroyed when a new scene is loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        /// <summary>
        /// Plays the hover sound.
        /// </summary>
        public void PlayHoverSound()
        {
            hoverSound.Play();
        }

        /// <summary>
        /// Plays the click sound.
        /// </summary>
        public void PlayClickSound()
        {
            clickSound.Play();
        }
    }
}

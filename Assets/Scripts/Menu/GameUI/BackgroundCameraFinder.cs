using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Menu.GameUI
{
    /// <summary>
    /// This script is used to find the background camera in the scene.
    /// </summary>
    public class BackgroundCameraFinder : MonoBehaviour
    {
        /// <summary>
        /// The background camera.
        /// </summary>
        private Camera _backgroundCamera;

        /// <summary>
        /// The video player on this GameObject.
        /// </summary>
        private VideoPlayer _videoPlayer;

        /// <summary>
        /// Finds the background camera in the scene.
        /// </summary>
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Returns the background camera.
        /// </summary>
        /// <returns>The background camera.</returns>
        public Camera GetBackgroundCamera()
        {
            return _backgroundCamera;
        }

        /// <summary>
        /// Attaches the video player to the background camera.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="mode">The load scene mode.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _backgroundCamera = FindObjectOfType<Camera>();

            if (_backgroundCamera != null)
            {
                _videoPlayer.targetCamera = _backgroundCamera;
            }
        }
    }
}
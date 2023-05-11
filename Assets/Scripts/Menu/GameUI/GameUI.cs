using UnityEngine;
using Menu.GameUI.MenuFrames;

namespace Menu.GameUI
{
    public class GameUI : MonoBehaviour
    {
        /// <summary>
        /// The background game UI object.
        /// </summary>
        public GameObject backgroundGameUIGo;

        /// <summary>
        /// The menu frames.
        /// </summary>
        public Frames menuFrames;

        /// <summary>
        /// Makes the object not be destroyed when a new scene is loaded.
        /// </summary>
        private void Awake()
        {
            DontDestroyOnLoad(transform.gameObject);
        }

        /// <summary>
        /// Makes the player list frame active.
        /// </summary>
        public void MakePlayerListFrameActive()
        {
            menuFrames.MakePlayerListFrameActive();
        }

    }
}

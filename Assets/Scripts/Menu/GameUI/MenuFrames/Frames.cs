using UnityEngine;

namespace Menu.GameUI.MenuFrames
{
    public class Frames : MonoBehaviour
    {
        /// <summary>
        /// The lobby frame game object.
        /// </summary>
        [SerializeField] private GameObject _playerListFrameGo;

        /// <summary>
        /// Makes the player list frame game object active.
        /// </summary>
        public void MakePlayerListFrameActive()
        {
            _playerListFrameGo.SetActive(true);
        }

        /// <summary>
        /// Makes the player list frame game object inactive.
        /// </summary>
        public void MakePlayerListInactive()
        {
            _playerListFrameGo.SetActive(false);
        }
    }
}
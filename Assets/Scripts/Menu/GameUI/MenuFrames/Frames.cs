using UnityEngine;

namespace Menu.GameUI.MenuFrames
{
    public class Frames : MonoBehaviour
    {
        /// <summary>
        /// The lobby frame game object.
        /// </summary>
        [SerializeField] private GameObject playerListFrameGo;
        
        /// <summary>
        /// The lobby server IP frame game object.
        /// </summary>
        [SerializeField] private GameObject serverIpGo;

        /// <summary>
        /// Makes the player list frame game object active.
        /// </summary>
        public void MakePlayerListFrameActive()
        {
            playerListFrameGo.SetActive(true);
        }

        /// <summary>
        /// Makes the player list frame game object inactive.
        /// </summary>
        public void MakePlayerListFrameInactive()
        {
            playerListFrameGo.SetActive(false);
        }
        
        /// <summary>
        /// Makes the server IP frame game object active.
        /// </summary>
        public void MakeServerIpFrameActive()
        {
            serverIpGo.SetActive(true);
        }

        /// <summary>
        /// Makes the server IP frame game object inactive.
        /// </summary>
        public void MakeServerIpFrameInactive()
        {
            serverIpGo.SetActive(false);
        }
    }
}
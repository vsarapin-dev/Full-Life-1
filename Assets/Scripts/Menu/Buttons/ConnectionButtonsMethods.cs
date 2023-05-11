using MainNetworkScripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Buttons
{
    /// <summary>
    /// Provides methods for handling connection-related actions in the main menu.
    /// </summary>
    public class ConnectionButtonsMethods : MonoBehaviour
    {
        [SerializeField] private GameObject hostJoinCanvas;
        [SerializeField] private GameObject playerNameCanvas;
        [SerializeField] private TMP_Text loginText;
        
        private MainMenuNetworkManager _mainMenuNetworkManager;

        [Inject]
        public void Initialize(MainMenuNetworkManager mainMenuNetworkManager)
        {
            _mainMenuNetworkManager = mainMenuNetworkManager;
        }

        /// <summary>
        /// Starts a host session on the local machine.
        /// </summary>
        public void StartHost()
        {
            _mainMenuNetworkManager.networkAddress = "localhost";
            _mainMenuNetworkManager.StartHost();
            _mainMenuNetworkManager.CreatePlayerManager();
        }

        /// <summary>
        /// Connects to a host session on the local machine.
        /// </summary>
        public void StartClient()
        {
            _mainMenuNetworkManager.networkAddress = "localhost";
            _mainMenuNetworkManager.StartClient();
        }
        
        /// <summary>
        /// Validates the player name entered by the user, and displays the host/join canvas if the name is valid.
        /// </summary>
        public void Login()
        {
            if (ValidatePlayerNameString())
            {
                hostJoinCanvas.SetActive(true);
                playerNameCanvas.SetActive(false);
            }
        }

        /// <summary>
        /// Validates the player name entered by the user and saves it to PlayerPrefs if it is valid.
        /// </summary>
        /// <returns>True if the player name is valid, false otherwise.</returns>
        private bool ValidatePlayerNameString()
        {
            if (loginText.text.Trim().Length > 4)
            {
                PlayerPrefs.SetString("playerName", loginText.text.Trim());
                return true;
            }
            return false;
        }
    }
}
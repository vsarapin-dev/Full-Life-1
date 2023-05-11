using MainNetworkScripts;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Buttons
{
    public class LobbyButtonsListeners : MonoBehaviour
    {
        [SerializeField] private Button buttonBack;
        [SerializeField] private Button buttonReady;
        
        private MainMenuNetworkManager _mainMenuNetworkManager;
        private ReadyButton _readyButtonScript;
        private bool _currentPlayerReadyStatus = false;

        public int NetworkId;
        
        /// <summary>
        ///     This method is called by Zenject to inject dependencies into the class.
        /// </summary>
        /// <param name="mainMenuNetworkManager">The main menu network manager.</param>
        [Inject]
        private void Initialize(MainMenuNetworkManager mainMenuNetworkManager)
        {
            _mainMenuNetworkManager = mainMenuNetworkManager;

        }

        /// <summary>
        ///     This method is called when the class is initialized.
        /// </summary>
        /// <remarks>
        ///     This method adds listeners for the ready and back buttons and cache ready button script.
        /// </remarks>
        private void Start()
        {
            _readyButtonScript = buttonReady.GetComponent<ReadyButton>();
            ChangeButtonReadyStatusText();

            buttonReady.onClick.AddListener(ButtonReadyClick);
            buttonBack.onClick.AddListener(ButtonBackClick);
        }

        /// <summary>
        ///     This method is called when the class is destroyed.
        /// </summary>
        /// <remarks>
        ///     This method removes listeners for the ready and back buttons.
        /// </remarks>
        private void OnDestroy()
        {
            buttonReady.onClick.RemoveListener(ButtonReadyClick);
            buttonBack.onClick.RemoveListener(ButtonBackClick);
        }

        /// <summary>
        ///     This method is called when the ready button is clicked.
        /// </summary>
        /// <remarks>
        ///     This method toggles the ready status of the local player and invokes the OnPlayerChangeLobbyStatus event.
        /// </remarks>
        private void ButtonReadyClick()
        {
            ChangeButtonReadyStatusText();
            _mainMenuNetworkManager.PlayerManager.CmdChangePlayerStatus(NetworkId);
        }

        /// <summary>
        ///     This method is called when the back button is clicked.
        /// </summary>
        private void ButtonBackClick()
        {
            if (_mainMenuNetworkManager.mode == NetworkManagerMode.Host)
            {
                _mainMenuNetworkManager.PlayerManager.CmdRemoveAllPlayersFromList();
            }
            else
            {
                _mainMenuNetworkManager.PlayerManager.CmdRemovePlayersFromList(NetworkId);
            }
        }

        private void ChangeButtonReadyStatusText()
        {
            _currentPlayerReadyStatus = !_currentPlayerReadyStatus;
            _readyButtonScript.SetAppropriateText(_currentPlayerReadyStatus);
        }
    }
}
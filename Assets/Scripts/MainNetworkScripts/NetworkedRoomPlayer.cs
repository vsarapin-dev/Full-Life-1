using Menu;
using Mirror;
using UnityEngine;
using Zenject;
using Menu.Buttons;

namespace MainNetworkScripts
{
    public class NetworkedRoomPlayer : NetworkRoomPlayer
    {
        [SyncVar(hook = nameof(PlayerNameChanged))]
        private string _playerName;
        private int _netId;
        private LobbyButtonsListeners _lobbyButtonsListeners;
        private MainMenuNetworkManager _mainMenuNetworkManager;
        
        public int NetId => _netId;

        [Inject]
        private void Initialize(
            LobbyButtonsListeners lobbyButtonsListeners,
            MainMenuNetworkManager mainMenuNetworkManager)
        {
            _lobbyButtonsListeners = lobbyButtonsListeners;
            _mainMenuNetworkManager = mainMenuNetworkManager;
        }

        #region Commands

        [Command]
        private void CmdSetPlayerName(string newPlayerName)
        {
            PlayerNameChanged(_playerName, newPlayerName);
        }
        
        #endregion

        #region Optional UI

        public override void OnGUI()
        {
            base.OnGUI();
        }

        #endregion

        #region Start & Stop Callbacks
        
        public override void OnStartAuthority()
        {
            base.OnStartAuthority();
            gameObject.name = "LocalPlayer";
            _netId = (int)netId;
            string playerName = PlayerPrefs.GetString("playerName");
            _lobbyButtonsListeners.NetworkId = _netId;
            _mainMenuNetworkManager.PlayerManager.CmdAddPlayersToList(_netId, playerName, false);
            CmdSetPlayerName(playerName);
        }

        #endregion

       #region SyncVar Hooks

        public override void IndexChanged(int oldIndex, int newIndex) {}

        public void PlayerNameChanged(string oldName, string newName)
        {
            _playerName = newName;
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) {}

        #endregion
    }
}
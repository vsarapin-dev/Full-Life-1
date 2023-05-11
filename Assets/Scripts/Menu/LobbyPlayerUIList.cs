using System;
using System.Collections.Generic;
using MainNetworkScripts;
using Mirror;
using Structs;
using UnityEngine;
using Zenject;

namespace Menu
{
    /// <summary>
    ///     Represents the list of players in the lobby.
    /// </summary>
    public class LobbyPlayerUIList : MonoBehaviour
    {
        [SerializeField] private PlayerListItem playerListItem;

        /// <summary>
        ///     The dictionary of instantiated player list items.
        /// </summary>
        private Dictionary<int, GameObject> _playerLobbyItems = new Dictionary<int, GameObject>();

        /// <summary>
        ///     The prefab for the player list item.
        /// </summary>

        private MainMenuNetworkManager _mainMenuNetworkManager;

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
        ///    Redraw player list items on the UI.
        /// </summary>
        public void RedrawLobbyPlayersList()
        {
            DestroyAllPlayerList();
            
            foreach (KeyValuePair<int,PlayersData> entry in _mainMenuNetworkManager.PlayerManager.PlayerList)
            {
                GameObject newPlayerListItem = Instantiate(playerListItem.gameObject, transform);
                newPlayerListItem.GetComponent<PlayerListItem>().InitializePlayerDataItem(entry.Value);
                _playerLobbyItems.Add(entry.Value.NetworkId, newPlayerListItem);
            }
            
            ShouldThisPlayerStopped();
        }
        
        /// <summary>
        ///     Change player status on the UI.
        /// </summary>
        /// ///<param name="networkId">The key associated with the PlayerListItem object.</param>
        public void RedrawLobbyPlayerStatus(int networkId)
        {
            _playerLobbyItems[networkId]
                .GetComponent<PlayerListItem>()
                .ChangeReadyStatusOnUI(_mainMenuNetworkManager.PlayerManager.PlayerList[networkId]);
        }


        /// <summary>
        /// Destroys all lobby items and clears the dictionary.
        /// </summary>
        private void DestroyAllPlayerList()
        {
            foreach (KeyValuePair<int, GameObject> entry in new Dictionary<int, GameObject>(_playerLobbyItems))
            {
                Destroy(entry.Value);
            }
            _playerLobbyItems.Clear();
        }

        /// <summary>
        /// Checks if the local player exists in the player list and stops the host/client if they do not exist.
        /// </summary>
        private void ShouldThisPlayerStopped()
        {
            int networkId = _mainMenuNetworkManager.LocalPlayer.NetId;

            if (_mainMenuNetworkManager.PlayerManager.PlayerList.ContainsKey(networkId) == false)
            {
                if (_mainMenuNetworkManager.mode == NetworkManagerMode.Host)
                {
                    _mainMenuNetworkManager.StopHost();
                }
                else
                {
                    _mainMenuNetworkManager.StopClient();
                }
            }
        }
    }
}
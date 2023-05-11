using Menu;
using Mirror;
using Structs;

namespace Player
{
    /// <summary>
    /// Manages player data and updates lobby UI based on changes in the player list.
    /// </summary>
    public class PlayerManager : NetworkBehaviour
    {
        /// <summary>
        /// Dictionary that stores player data, synchronized across clients.
        /// </summary>
        public readonly SyncDictionary<int, PlayersData> PlayerList = new SyncDictionary<int, PlayersData>();
        
        /// <summary>
        /// The LobbyPlayerUIList instance used for managing the lobby UI.
        /// </summary>
        private LobbyPlayerUIList _lobbyPlayerUIList;

        public override void OnStartClient()
        {
            PlayerList.Callback += OnPlayerListChange;
        }
        
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
           DontDestroyOnLoad(gameObject);
        }


        /// <summary>
        /// Finds the lobby player UI list component if it hasn't been assigned yet.
        /// </summary>
        private void FindLobbyPlayerUIList()
        {
            _lobbyPlayerUIList = FindObjectOfType<LobbyPlayerUIList>();
        }

        /// <summary>
        /// Callback function that gets called when the player list changes.
        /// </summary>
        /// <param name="op">The operation that caused the change.</param>
        /// <param name="networkId">The network ID of the player that was affected.</param>
        /// <param name="item">The new data for the affected player.</param>
        private void OnPlayerListChange(SyncIDictionary<int, PlayersData>.Operation op, int networkId, PlayersData item)
        {
            if (_lobbyPlayerUIList == null) FindLobbyPlayerUIList();

            switch (op)
            {
                case SyncIDictionary<int, PlayersData>.Operation.OP_ADD:
                    _lobbyPlayerUIList.RedrawLobbyPlayersList();
                    break;
                case SyncIDictionary<int, PlayersData>.Operation.OP_SET:
                    _lobbyPlayerUIList.RedrawLobbyPlayerStatus(networkId);
                    break;
                case SyncIDictionary<int, PlayersData>.Operation.OP_REMOVE:
                    _lobbyPlayerUIList.RedrawLobbyPlayersList();
                    break;
                case SyncIDictionary<int, PlayersData>.Operation.OP_CLEAR:
                    _lobbyPlayerUIList.RedrawLobbyPlayersList();
                    break;
            }
        }
        
        #region Commands

        /// <summary>
        /// Adds a player to the player list.
        /// </summary>
        /// <param name="playerNetworkId">The network ID of the player to add.</param>
        /// <param name="playerName">The name of the player to add.</param>
        /// <param name="isReady">Whether or not the player is ready.</param>
        [Command(requiresAuthority = false)]
        public void CmdAddPlayersToList(
            int playerNetworkId,
            string playerName,
            bool isReady)
        {
            if(!PlayerList.ContainsKey(playerNetworkId))
            {
                PlayersData playerData = new PlayersData(playerNetworkId,playerName,isReady);
                PlayerList.Add(playerNetworkId, playerData);
            }
        }
        
        /// <summary>
        /// Removes a player from the player list.
        /// </summary>
        /// <param name="playerNetworkId">The network ID of the player to remove.</param>
        [Command(requiresAuthority = false)]
        public void CmdRemovePlayersFromList(int playerNetworkId)
        {
            PlayerList.Remove(playerNetworkId);
        }
        
        /// <summary>
        /// Removes all players from the player list
        /// </summary>
        [Command(requiresAuthority = false)]
        public void CmdRemoveAllPlayersFromList()
        {
            PlayerList.Clear();
        }
        
        /// <summary>
        /// Change player status on dict
        /// </summary>
        [Command(requiresAuthority = false)]
        public void CmdChangePlayerStatus(int playerNetworkId)
        {
            PlayersData player = PlayerList[playerNetworkId];
            player.IsReady = !player.IsReady;
            PlayerList[playerNetworkId] = player;
        }

        #endregion
    }
}
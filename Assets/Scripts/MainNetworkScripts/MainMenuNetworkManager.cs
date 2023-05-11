using Mirror;
using Player;
using UnityEngine;

namespace MainNetworkScripts
{
    public class MainMenuNetworkManager : NetworkRoomManager
    {
        [SerializeField] private GameObject playerManager;

        private PlayerManager _playerManager;
        private NetworkedRoomPlayer _localPlayer;
        
        public PlayerManager PlayerManager
        {
            get
            {
                if (_playerManager != null)
                {
                    return _playerManager;
                }
                else
                {
                    _playerManager = FindObjectOfType<PlayerManager>();
                }

                return _playerManager;
            }
            
            set => _playerManager = value;
        }
        public NetworkedRoomPlayer LocalPlayer
        {
            get
            {
                if (_localPlayer != null)
                {
                    return _localPlayer;
                }
                else
                {
                    _localPlayer = GameObject.Find("LocalPlayer").GetComponent<NetworkedRoomPlayer>();
                }

                return _localPlayer;
            }
            
            set => _localPlayer = value;
        }

        #region Server Callbacks

        public override void OnRoomStartServer() {}
        public override void OnRoomStopServer() {}
        public override void OnRoomStartHost() {}

        public override void OnRoomStopHost() { }
        public override void OnRoomServerConnect(NetworkConnectionToClient conn) { }
        public override void OnRoomServerDisconnect(NetworkConnectionToClient conn) { }
        public override void OnRoomServerSceneChanged(string sceneName) { }

        public void CreatePlayerManager()
        {
            GameObject playerManagerGO = Instantiate(playerManager);
            NetworkServer.Spawn(playerManagerGO);
        }

        /// <summary>
        /// This allows customization of the creation of the room-player object on the server.
        /// <para>By default the roomPlayerPrefab is used to create the room-player, but this function allows that behaviour to be customized.</para>
        /// </summary>
        /// <param name="conn">The connection the player object is for.</param>
        /// <returns>The new room-player object.</returns>
        public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
        {
            return base.OnRoomServerCreateRoomPlayer(conn);
        }

        /// <summary>
        /// This allows customization of the creation of the GamePlayer object on the server.
        /// <para>By default the gamePlayerPrefab is used to create the game-player, but this function allows that behaviour to be customized. The object returned from the function will be used to replace the room-player on the connection.</para>
        /// </summary>
        /// <param name="conn">The connection the player object is for.</param>
        /// <param name="roomPlayer">The room player object for this connection.</param>
        /// <returns>A new GamePlayer object.</returns>
        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
        {
            return base.OnRoomServerCreateGamePlayer(conn, roomPlayer);
        }

        /// <summary>
        /// This allows customization of the creation of the GamePlayer object on the server.
        /// <para>This is only called for subsequent GamePlay scenes after the first one.</para>
        /// <para>See OnRoomServerCreateGamePlayer to customize the player object for the initial GamePlay scene.</para>
        /// </summary>
        /// <param name="conn">The connection the player object is for.</param>
        public override void OnRoomServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnRoomServerAddPlayer(conn);
        }

        /// <summary>
        /// This is called on the server when it is told that a client has finished switching from the room scene to a game player scene.
        /// <para>When switching from the room, the room-player is replaced with a game-player object. This callback function gives an opportunity to apply state from the room-player to the game-player object.</para>
        /// </summary>
        /// <param name="conn">The connection of the player</param>
        /// <param name="roomPlayer">The room player object.</param>
        /// <param name="gamePlayer">The game player object.</param>
        /// <returns>False to not allow this player to replace the room player.</returns>
        public override bool OnRoomServerSceneLoadedForPlayer(NetworkConnectionToClient conn, GameObject roomPlayer, GameObject gamePlayer)
        {
            return base.OnRoomServerSceneLoadedForPlayer(conn, roomPlayer, gamePlayer);
        }

        /// <summary>
        /// This is called on server from NetworkRoomPlayer.CmdChangeReadyState when client indicates change in Ready status.
        /// </summary>
        public override void ReadyStatusChanged()
        {
            base.ReadyStatusChanged();
        }

        /// <summary>
        /// This is called on the server when all the players in the room are ready.
        /// <para>The default implementation of this function uses ServerChangeScene() to switch to the game player scene. By implementing this callback you can customize what happens when all the players in the room are ready, such as adding a countdown or a confirmation for a group leader.</para>
        /// </summary>
        public override void OnRoomServerPlayersReady()
        {
            base.OnRoomServerPlayersReady();
        }

        /// <summary>
        /// This is called on the server when CheckReadyToBegin finds that players are not ready
        /// <para>May be called multiple times while not ready players are joining</para>
        /// </summary>
        public override void OnRoomServerPlayersNotReady() { }

        #endregion

        #region Client Callbacks

        /// <summary>
        /// This is a hook to allow custom behaviour when the game client enters the room.
        /// </summary>
        public override void OnRoomClientEnter() { }

        /// <summary>
        /// This is a hook to allow custom behaviour when the game client exits the room.
        /// </summary>
        public override void OnRoomClientExit() { }

        /// <summary>
        /// This is called on the client when it connects to server.
        /// </summary>
        public override void OnRoomClientConnect() { }

        /// <summary>
        /// This is called on the client when disconnected from a server.
        /// </summary>
        public override void OnRoomClientDisconnect() { }

        /// <summary>
        /// This is called on the client when a client is started.
        /// </summary>
        public override void OnRoomStartClient() { }

        /// <summary>
        /// This is called on the client when the client stops.
        /// </summary>
        public override void OnRoomStopClient() { }

        /// <summary>
        /// This is called on the client when the client is finished loading a new networked scene.
        /// </summary>
        public override void OnRoomClientSceneChanged() { }

        #endregion

        #region Optional UI

        public override void OnGUI()
        {
            base.OnGUI();
        }

        #endregion
    }
}

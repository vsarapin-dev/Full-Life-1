using UnityEngine;

namespace Mirror
{
    /// <summary>
    /// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
    /// <para>The RoomPrefab object of the NetworkRoomManager must have this component on it. This component holds basic room player data required for the room to function. Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.</para>
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Network/Network Room Player")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-room-player")]
    public class NetworkRoomPlayer : NetworkBehaviour
    {
        [Tooltip("This flag controls whether the default UI is shown for the room player")]
        public bool showRoomGUI = true;

        [Header("Diagnostics")]

        [Tooltip("Diagnostic flag indicating whether this player is ready for the game to begin")]
        [SyncVar(hook = nameof(ReadyStateChanged))]
        public bool readyToBegin;
        

        [Tooltip("Diagnostic index of the player, e.g. Player1, Player2, etc.")]
        [SyncVar(hook = nameof(IndexChanged))]
        public int index;

        #region Unity Callbacks

        /// <summary>
        /// Do not use Start - Override OnStartHost / OnStartClient instead!
        /// </summary>
        public void Start()
        {
            if (NetworkManager.singleton is NetworkRoomManager room)
            {
                // NetworkRoomPlayer object must be set to DontDestroyOnLoad along with NetworkRoomManager
                // in server and all clients, otherwise it will be respawned in the game scene which would
                // have undesirable effects.
                if (room.dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);

                room.roomSlots.Add(this);

                if (NetworkServer.active)
                    room.RecalculateRoomPlayerIndices();

                if (NetworkClient.active)
                    room.CallOnClientEnterRoom();
            }
            else Debug.LogError("RoomPlayer could not find a NetworkRoomManager. The RoomPlayer requires a NetworkRoomManager object to function. Make sure that there is one in the scene.");
        }

        public virtual void OnDisable()
        {
            if (NetworkClient.active && NetworkManager.singleton is NetworkRoomManager room)
            {
                // only need to call this on client as server removes it before object is destroyed
                room.roomSlots.Remove(this);

                room.CallOnClientExitRoom();
            }
        }

        #endregion

        #region Commands

        [Command]
        public void CmdChangeReadyState(bool readyState)
        {
            readyToBegin = readyState;
            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room != null)
            {
                room.ReadyStatusChanged();
            }
        }

        #endregion

        #region SyncVar Hooks

        public virtual void IndexChanged(int oldIndex, int newIndex) {}

        public virtual void ReadyStateChanged(bool oldReadyState, bool newReadyState) {}

        #endregion

        #region Room Client Virtuals

        public virtual void OnClientEnterRoom() {}

        public virtual void OnClientExitRoom() {}

        #endregion

        #region Optional UI

        public virtual void OnGUI()
        {
            if (!showRoomGUI)
                return;

            NetworkRoomManager room = NetworkManager.singleton as NetworkRoomManager;
            if (room)
            {
                if (!room.showRoomGUI)
                    return;

                if (!Utils.IsSceneActive(room.RoomScene))
                    return;

                DrawPlayerReadyState();
                DrawPlayerReadyButton();
            }
        }

        void DrawPlayerReadyState()
        {
            GUILayout.BeginArea(new Rect(20f + (index * 100), 200f, 90f, 130f));

            GUILayout.Label($"Player [{index + 1}]");

            if (readyToBegin)
                GUILayout.Label("Ready");
            else
                GUILayout.Label("Not Ready");

            if (((isServer && index > 0) || isServerOnly) && GUILayout.Button("REMOVE"))
            {
                // This button only shows on the Host for all players other than the Host
                // Host and Players can't remove themselves (stop the client instead)
                // Host can kick a Player this way.
                GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }

            GUILayout.EndArea();
        }

        void DrawPlayerReadyButton()
        {
            if (NetworkClient.active && isLocalPlayer)
            {
                GUILayout.BeginArea(new Rect(20f, 300f, 120f, 20f));

                if (readyToBegin)
                {
                    if (GUILayout.Button("Cancel"))
                        CmdChangeReadyState(false);
                }
                else
                {
                    if (GUILayout.Button("Ready"))
                        CmdChangeReadyState(true);
                }

                GUILayout.EndArea();
            }
        }

        #endregion
    }
}

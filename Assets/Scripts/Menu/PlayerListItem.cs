using Structs;
using TMPro;
using UnityEngine;

namespace Menu
{
    /// <summary>
    /// Represents a player item in the list of players.
    /// </summary>
    public class PlayerListItem : MonoBehaviour
    {
        /// <summary>
        /// Represents player data.
        /// </summary>
        private PlayersData _playerData;
        
        /// <summary>
        /// The text field for displaying the player's login.
        /// </summary>
        ///
        [SerializeField] private TMP_Text loginText;
        
        /// <summary>
        /// The icon that indicates the player is not ready.
        /// </summary>
        [SerializeField] private GameObject notReadyIcon;
        
        /// <summary>
        /// The icon that indicates the player is ready.
        /// </summary>
        [SerializeField] private GameObject readyIcon;

        /// <summary>
        /// Sets the playerData object to private field.
        /// </summary>
        /// <param name="playerData">The login of the player.</param>
        public void InitializePlayerDataItem(PlayersData playerData)
        {
            _playerData = playerData;
            SetLoginText();
            SetReadyStatusOnUI();
        }

        /// <summary>
        /// Changes the player's ready status on the UI.
        /// </summary>
        /// <param name="playerData">The data of the player whose status is being changed.</param>
        public void ChangeReadyStatusOnUI(PlayersData playerData)
        {
            _playerData = playerData;
            SetReadyStatusOnUI();
        }

        /// <summary>
        /// Sets the login text to display the player's login.
        /// </summary>
        private void SetLoginText()
        {
            loginText.text = _playerData.PlayerName;
        }
        
        /// <summary>
        /// Sets player ready on not ready icon on UI.
        /// </summary>
        private void SetReadyStatusOnUI()
        {
            switch (_playerData.IsReady)
            {
                case true:
                    notReadyIcon.SetActive(false);
                    readyIcon.SetActive(true);
                    break;
                case false:
                    readyIcon.SetActive(false);
                    notReadyIcon.SetActive(true);
                    break;
            }
        }
    }
}
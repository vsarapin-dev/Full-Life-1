using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Sounds;
using MainNetworkScripts;
using Mirror;

namespace Menu.Buttons
{
    /// <summary>
    ///  This script is used to add events to the text buttons in the menu
    /// <summary>
    public class MenuButtons : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        /// <summary>
        /// The current button name.
        /// </summary>
        private string _currentButtonName;

        /// <summary>
        /// The current button object.
        /// </summary>
        private TMP_Text _currentButtonObject;

        /// <summary>
        /// The current button audio source.
        /// </summary>
        private AudioSource _buttonAudioSource;

        /// <summary>
        /// The mouse over color.
        /// </summary>
        private Color _mouseOverColor = Color.grey;

        /// <summary>
        /// The original button color.
        /// </summary>
        private Color _originalColor;

        /// <summary>
        /// The UISounds class.
        /// </summary>
        private UISounds _uiSounds;

        /// <summary>
        /// The MainMenuNetworkManager class.
        /// </summary>
        private MainMenuNetworkManager _mainMenuNetworkManager;

        /// <summary>
        /// Injects the UISounds class.
        /// </summary>
        /// <param name="uiSounds">The UISounds class.</param>
        /// <param name="mainMenuNetworkManager">The MainMenuNetworkManager class.</param>
        [Inject]
        private void Initialize(UISounds uiSounds, MainMenuNetworkManager mainMenuNetworkManager)
        {
            _uiSounds = uiSounds;
            _mainMenuNetworkManager = mainMenuNetworkManager;
        }

        /// <summary>
        /// Sets the current button name, object, and audio source.
        /// </summary>
        private void Awake()
        {
            Debug.Log(NetworkServer.active);
            _currentButtonName = name.ToLower();
            _currentButtonObject = GetComponent<TMPro.TMP_Text>();
            _buttonAudioSource = GetComponent<AudioSource>();
            _originalColor = GetComponent<TMPro.TMP_Text>().color;
        }

        /// <summary>
        /// When text in menu clicked.
        /// </summary>
        /// <param name="pointerEventData"></param>
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            _uiSounds.PlayClickSound();
            AppropriateAction();
            Debug.Log(name);
        }

        /// <summary>
        /// When mouse hovers over text in menu.
        /// </summary>
        /// <param name="data"></param>
        public void OnPointerEnter(PointerEventData data)
        {
            _uiSounds.PlayHoverSound();
            _currentButtonObject.color = _mouseOverColor;
        }

        /// <summary>
        /// When mouse leaves text in menu.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            _currentButtonObject.color = _originalColor;
        }

        /// <summary>
        /// Choose the appropriate action based on the name of the button
        /// </summary>
        private void AppropriateAction()
        {
            switch (true)
            {
                case object _ when _currentButtonName.Contains("new"):
                    StartHost();
                    break;
                case object _ when _currentButtonName.Contains("join"):
                    StartClient();
                    break;
                case object _ when _currentButtonName.Contains("exit"):
                    break;
            }
        }

        /// <summary>
        /// Starts a host session on the local machine.
        /// </summary>
        private void StartHost()
        {
            _mainMenuNetworkManager.networkAddress = "localhost";
            _mainMenuNetworkManager.StartHost();
            _mainMenuNetworkManager.CreatePlayerManager();
        }

        /// <summary>
        /// Connects to a host session on the local machine.
        /// </summary>
        private void StartClient()
        {
            _mainMenuNetworkManager.networkAddress = "localhost";
            _mainMenuNetworkManager.StartClient();
        }

        /// <summary>
        /// Exits the game.
        /// </summary>
        private void Exit()
        {
            ///TODO: Add exit functionality
        }
    }
}
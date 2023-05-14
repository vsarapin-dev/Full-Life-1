using System.Text.RegularExpressions;
using MainNetworkScripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu.Inputs
{
    
    /// <summary>
    /// A custom input field for entering IP addresses with optional port number.
    /// </summary>
    public class IPInputField : MonoBehaviour
    {
        private TMP_InputField _inputField;
        private Regex _regex;
        private MainMenuNetworkManager _mainMenuNetworkManager;

        [Inject]
        private void Initialize(MainMenuNetworkManager mainMenuNetworkManager)
        {
            _mainMenuNetworkManager = mainMenuNetworkManager;
        }
        
        /// <summary>
        /// Initializes the input field and sets up the regular expression pattern.
        /// </summary>
        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
            _regex = new Regex(@"^(?:\d{1,3}\.){0,3}\d{0,3}(?::\d{0,5})?$");
        }

        /// <summary>
        /// Enables the input field and sets up the validation callback.
        /// </summary>
        private void OnEnable()
        {
            _inputField.onValidateInput += ValidateInput;
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        /// <summary>
        /// Disables the input field and removes the validation callback.
        /// </summary>
        private void OnDisable()
        {
            _inputField.onValidateInput -= ValidateInput;
            _inputField.onEndEdit.RemoveListener(OnEndEdit);
        }

        /// <summary>
        /// Validates input text and restricts it to IP address format with optional port number.
        /// </summary>
        /// <param name="text">The current input text.</param>
        /// <param name="charIndex">The index of the current character being added.</param>
        /// <param name="addedChar">The character being added to the input field.</param>
        /// <returns>The validated character to add to the input field, or null if invalid.</returns>
        private char ValidateInput(string text, int charIndex, char addedChar)
        {
            string newText = text.Substring(0, charIndex) + addedChar + text.Substring(charIndex);

            if (!_regex.IsMatch(newText))
            {
                return '\0';
            }

            return addedChar;
        }
        
        private void OnEndEdit(string value)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                _mainMenuNetworkManager.networkAddress = "localhost";
                _mainMenuNetworkManager.StartClient();
            }
        }
    }
}
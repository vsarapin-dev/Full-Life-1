using Menu.Buttons;
using Menu.GameUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneChangeListener : MonoBehaviour
{
    /// <summary>
    /// The game UI.
    /// </summary>
    private GameUI _gameUI;


    [Inject]
    private void Initialize(GameUI gameUI)
    {
        _gameUI = gameUI;
    }

    /// <summary>
    /// Makes the object not be destroyed when a new scene is loaded.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /// <summary>
    /// Makes the player list frame active when the lobby scene is loaded.
    /// Makes the menu buttons inactive when the lobby scene is loaded.
    /// Makes the menu buttons active when the main menu scene is loaded.
    /// </summary>
    /// <param name="scene">The scene that was loaded.</param>
    /// <param name="mode">The load scene mode.</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
        {
            _gameUI.MakePlayerListFrameActive();
        }
    }

}
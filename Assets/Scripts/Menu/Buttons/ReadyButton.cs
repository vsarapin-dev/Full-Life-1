using UnityEngine;

public class ReadyButton : MonoBehaviour {
    
    [SerializeField] private GameObject becomeReadyTextGo;
    [SerializeField] private GameObject becomeNotReadyTextGo;

    /// <summary>
    ///  Set appropriate text on button.
    /// </summary>
    /// <param name="isReady">Whether or not the player is ready.</param>
    public void SetAppropriateText(bool isReady)
    {
        becomeReadyTextGo.SetActive(isReady);
        becomeNotReadyTextGo.SetActive(!isReady);
    }
}
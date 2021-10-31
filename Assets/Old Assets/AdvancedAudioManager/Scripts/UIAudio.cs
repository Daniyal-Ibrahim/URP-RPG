using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // All our variables...
    [Header("UI Audio Manager")]
    public string playWhenEnterHovering;
    public string playWhenClicked;
    public string playWhenExitHovering;
    private AdvancedAudioManager advancedAudioManager;

    // Runs before first frame...
    private void Start()
    {
        // Find Advanced Audio Manager in our scene...
        advancedAudioManager = FindObjectOfType<AdvancedAudioManager>();
    }

    // Checks if pointer clicked our button...
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // If our sound to play slot for this is empty, don't do anything...
        if (playWhenClicked == null)
            return;

        // Otherwise, if it isn't empty, play the sound!
        advancedAudioManager.PlaySound(playWhenClicked);
    }

    // Check if pointer hovered over our button...
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // If our sound to play slot for this is empty, don't do anything...
        if (playWhenEnterHovering == null)
            return;

        // Otherwise, if it isn't empty, play the sound!
        advancedAudioManager.PlaySound(playWhenEnterHovering);
    }

    // Check if pointer exited hovering over our button...
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        // If our sound to play slot for this is empty, don't do anything...
        if (playWhenExitHovering == null)
            return;

        // Otherwise, if it isn't empty, play the sound!
        advancedAudioManager.PlaySound(playWhenExitHovering);
    }

}

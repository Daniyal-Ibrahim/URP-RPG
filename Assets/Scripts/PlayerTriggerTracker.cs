using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Interfaces;

public class PlayerTriggerTracker : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.ShowInteractionPopup();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            if (player.inInteracting)
            {
                player.inInteracting = false;
                interactable.StartInteraction();    
                interactable.HideInteractionPopup();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.StopInteraction();
            interactable.HideInteractionPopup();
        }
    }
}

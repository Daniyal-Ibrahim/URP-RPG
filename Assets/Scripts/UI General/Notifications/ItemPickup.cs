using _Scripts.Events.CustomEvents;
using _Scripts.Inventory_System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemEvent onEnter;
    [SerializeField] private VoidEvent onExit;
    [SerializeField] private ItemSlot slot;


    [Button]
    private void Enter()
    {
        onEnter.Raise(slot);
    }

    [Button]
    private void Exit()
    {
        onExit.Raise();
    }
}

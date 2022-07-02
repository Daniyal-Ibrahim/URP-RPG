using _Scripts.Inventory_System;
using System;
using UnityEngine;

namespace _Scripts.Events.CustomEvents
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "Game Events/Item Event")]
    public class ItemEvent : BaseGameEvent<ItemSlot>
    {
        
        public void Raise(ItemSlot slot)
        {
            RaiseEvent(slot);
        }

    }


}
        
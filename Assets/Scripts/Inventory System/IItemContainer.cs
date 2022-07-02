using System;

namespace _Scripts.Inventory_System
{
    public interface IItemContainer
    {
        ItemSlot AddItem(ItemSlot ItemToAdd);
        
        void RemoveItem(ItemSlot itemSlot);
        
        void RemoveAt(int slotIndex);
        
        void Swap(int slotA, int slotB);
        
        bool HasItem(Item item);
        
        int GetTotalQuantity(Item item);
    }
}
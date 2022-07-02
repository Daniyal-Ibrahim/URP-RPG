using System;
using Sirenix.OdinInspector;

namespace _Scripts.Inventory_System
{
    [Serializable]
    public struct ItemSlot
    {
        public Item item;
        public int quantity;

        public ItemSlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            return a.Equals(b);
        }
        
        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return !a.Equals(b);
        }

        public int CompareTo(ItemSlot other)
        {
            if (other == null)
            {
                return 1;
            }

            return 0;
        }
    }
}
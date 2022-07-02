using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    public class InventorySystem : MonoBehaviour, IItemContainer
    {
        public ItemDatabase database;
        
        public List<ItemSlot> itemSlots;
        public VoidEvent onInventoryUpdated;
        
        public ItemSlot GetSlotByIndex(int index)
        {
            return itemSlots[index];
        }

        public ItemSlot AddItem(ItemSlot itemToAdd)
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                // check if slot is not empty
                if (itemSlots[i].item != null)
                {
                    // check if the item to add already exits in inventory
                    if (itemSlots[i].item == itemToAdd.item)
                    {
                        // check if it is at max stacks
                        if (itemSlots[i].quantity != itemSlots[i].item.MaxStack)
                        {
                            Debug.Log("Adding existing item");

                            var amountAddable = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                            var temp = itemSlots[i];
                            //if(itemSlots[i].Quantity + itemToAdd.Quantity <= itemSlots[i].Item.MaxStack)
                            if (itemToAdd.quantity <= amountAddable)
                            {
                                temp.quantity += itemToAdd.quantity;
                                itemSlots[i] = temp;

                                onInventoryUpdated.Raise();
                                return itemToAdd;
                            }
                            // TODO: make this loop through total quantity and assigns slots accordingly
                            // add difference and place rest in a new slot
                            if (itemSlots[i].quantity + amountAddable <= itemSlots[i].item.MaxStack)
                            {
                                temp.quantity += amountAddable;
                                itemSlots[i] = temp;
                                // split into new stack if space is available else do noting 
                                for (var j = 0; j < itemSlots.Count; j++)
                                {
                                    if (itemSlots[j].item == null)
                                    {
                                        if (itemToAdd.quantity - amountAddable <= itemToAdd.item.MaxStack)
                                        {
                                            var newSlot = itemToAdd;
                                            newSlot.quantity -= amountAddable;// itemToAdd.Item.MaxStack;

                                            Debug.Log("Adding to Stack and making new item");
                                            itemSlots[j] = newSlot;
                                            
                                            onInventoryUpdated.Raise();
                                            return itemToAdd;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            
            for (var i = 0; i < itemSlots.Count; i++)
            {
                // Find the 1st empty slot and place the item there
                if (itemSlots[i].item == null)
                {
                    if (itemToAdd.quantity <= itemToAdd.item.MaxStack)
                    {
                        Debug.Log("Adding new item");
                        itemSlots[i] = itemToAdd;
                            
                        onInventoryUpdated.Raise();
                        return itemToAdd;
                    }
                    if (itemToAdd.quantity >= itemToAdd.item.MaxStack)
                    {
                        Debug.Log("Adding new item and splitting remaining");
                        var temp = itemSlots[i];
                        temp.item = itemToAdd.item;
                        temp.quantity = itemToAdd.item.MaxStack; 
                        itemSlots[i] = temp;
                        // split stack and add
                        for (var j = 0; j < itemSlots.Count; j++)
                        {
                            if (itemSlots[j].item == null)
                            {
                                var newSlot = itemToAdd;
                                newSlot.quantity -= itemToAdd.item.MaxStack;
                                itemSlots[j] = newSlot;

                                onInventoryUpdated.Raise();
                                return itemToAdd;
                            }
                        }
                        
                        onInventoryUpdated.Raise();
                        return itemToAdd;
                    }
                }
            }

            onInventoryUpdated.Raise();
            //OnItemsUpdated.Invoke();
            return itemToAdd;
        }

        public void RemoveItem(ItemSlot itemSlot)
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                /*if (_itemSlots[i].Item == null) continue;
                if (_itemSlots[i].Item != itemSlot.Item) continue;*/
                if (itemSlots.Contains(itemSlot))
                {
                    var slot = itemSlots[i];
                    if (slot.quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= slot.quantity;

                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        slot.quantity -= itemSlot.quantity;

                        if (slot.quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();
                        }
                    }
                }

                
            }
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > itemSlots.Count -1) 
            {
                return;
            }

            itemSlots[slotIndex] = new ItemSlot();
            onInventoryUpdated.Raise();
            //OnItemsUpdated.Invoke();
        }
        public void Swap(int slotA, int slotB)
        {
            var itemA = itemSlots[slotA];
            var itemB = itemSlots[slotB];

            if (itemA == itemB) return;

            if (itemB.item != null)
            {
                if (itemA.item == itemB.item)
                {
                    var value = itemB.item.MaxStack - itemB.quantity;

                    if (itemA.quantity <= value)
                    {
                        itemB.quantity += itemA.quantity;
                        itemSlots[slotA] = new ItemSlot();


                        onInventoryUpdated.Raise();
                        //OnItemsUpdated.Invoke();
                        return;
                    }
                }
            }

            itemSlots[slotA] = itemB;
            itemSlots[slotB] = itemA;
            
            onInventoryUpdated.Raise();
        }
        public void MergeQuantity(int slotA, int slotB)
        {
            var itemA = itemSlots[slotA]; // the item being dragged
            var itemB = itemSlots[slotB]; // the item dragged into
            
            // Merging the quantity of slot A and B
            if (itemA.quantity != itemA.item.MaxStack)
            {
                var maxAddable = itemA.item.MaxStack - itemA.quantity;

                if (itemB.quantity <= maxAddable)
                {
                    // add the amount and remove item B
                    itemA.quantity += itemB.quantity;

                    itemSlots[slotA] = new ItemSlot();
                    itemSlots[slotB] = itemA;
            
                    onInventoryUpdated.Raise();
                    return;
                }
                else
                {
                    // check how much we can add to the stack and update item B quantity 
                    itemA.quantity += maxAddable;
                    itemB.quantity -= maxAddable;
                    
                    itemSlots[slotA] = itemA;
                    itemSlots[slotB] = itemB;
            
                    onInventoryUpdated.Raise();
                    return;

                }


            }

            onInventoryUpdated.Raise();
        }

        public bool HasItem(Item item)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item == null) continue;
                if (slot.item != item) continue;

                return true;
            }

            return false;
        }

        public int GetTotalQuantity(Item item)
        {
            var totalCount = 0;

            foreach (var slot in itemSlots)
            {
                if (slot.item == null) continue;
                if (slot.item != item) continue;

                totalCount += slot.quantity;
            }
            
            return totalCount;
        }
        
        //public ItemSlot testItemSlot;
        [Button]
        public void TestAdd(int key,int amount)
        {
            foreach (var item in from data in database.database where data.key == key select new ItemSlot(data.value, amount))
            {
                AddItem(item);
            }
        }
        [Button]
        public void TestClear()
        {
            ClearItemSlots();
        }
        [Button]
        private void ClearItemSlots()
        {
            for (var i = 0; i < itemSlots.Count; i++)
            {
                itemSlots[i] = new ItemSlot();
            }
        }
        
    }
}
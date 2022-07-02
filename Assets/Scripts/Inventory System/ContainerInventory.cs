using _Scripts.Events.CustomEvents;
using _Scripts.Interfaces;
using _Scripts.Inventory_System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Inventory_System
{
    public class ContainerInventory : InventorySystem,IInteractable
    {
        [SerializeField] private VoidEvent onEnter;
        [SerializeField] private VoidEvent onExit;
        [SerializeField] private bool isIntractable;
        
        public GameObject containerUI;
        public GameObject containerPanel;
        public bool lootGenerated;

        private int _size;
        private int _itemList;
        
        [Button("Show Container")]
        public void StartInteraction()
        {

            ClearContainer();
            if (!lootGenerated)
            {
                GenerateLoot();
            }
            UpdateInventoryUI(_size);
            // Open Item Container 
            containerUI.transform.DOScale(Vector3.one, 0.5f);
            print("Interacting");
        }

        public void StopInteraction()
        {
            ClearContainer();
            containerUI.transform.DOScale(Vector3.zero, 0.5f);
        }

        [Button]
        public void GenerateLoot()
        {
            lootGenerated = true;
            _size = Random.Range(5, 21);
            _itemList = Random.Range(5, _size);

            for (var i = 0; i < _size; i++)
            {
                itemSlots.Add(new ItemSlot(null,0));
            }
            
            for (var i = 0; i < _itemList; i++)
            {
                var item = Random.Range(0, database.database.Count);
                var itemSlot = new ItemSlot
                {
                    item = database.database[item].value,
                    quantity = 1
                };
                itemSlots.RemoveAt(i);
                itemSlots.Insert(i,itemSlot);
                //AddItem(itemSlot);
            }
        }

        private void UpdateInventoryUI(int size)
        {
            var panel = containerPanel.GetComponent<InventoryPanel>();
            panel.inventorySystem = this;
            panel.SetInventorySize(size);
        }

        private void ClearContainer()
        {
            var count = containerPanel.transform.childCount;
            var panel = containerPanel.GetComponent<InventoryPanel>();
            panel.inventorySystem = null;

            for (var i = 0; i < count; i++)
            {
                var obj = containerPanel.transform.GetChild(i);
                Destroy(obj.gameObject);
            }
            
        }

        public void ShowInteractionPopup()
        {
            isIntractable = true;
            onEnter.Raise();
        }

        public void HideInteractionPopup()
        {
            isIntractable = false;
            onExit.Raise();
        }
        
        
    }
}
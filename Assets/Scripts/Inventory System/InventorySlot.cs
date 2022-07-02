using System;
using _Scripts.UI_General;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

namespace _Scripts.Inventory_System
{
    public class InventorySlot : ItemSlotUI, IPointerEnterHandler, IPointerExitHandler
    {
        public InventorySystem inventorySystem;
        [SerializeField] private TMP_Text itemQuantity;

        // the item who's data is displayed, item exits in an inventory
        private ItemSlot ItemSlot
        {
            get => inventorySystem.GetSlotByIndex(slotIndex);
            set => throw new NotImplementedException();
        }

        public override Item SlotItem
        {
            get => ItemSlot.item;
            set => throw new NotImplementedException();
        }

        public override void UpdateSlotUI()
        {
            if (ItemSlot.item == null)
            {
                EnableSlotUI(false);
            }
            else
            {
                EnableSlotUI(true);
                itemIcon.sprite = ItemSlot.item.Icon;
                itemQuantity.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
            }
            
        }
        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantity.enabled = enable;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            var itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if (itemDragHandler == null) return;

            if ((InventorySlot) itemDragHandler.ItemSlotUI == null) return;
            if(itemDragHandler.ItemSlotUI.slotIndex == slotIndex) return;
            if (itemDragHandler.ItemSlotUI.SlotItem == SlotItem)
            {
                inventorySystem.MergeQuantity(itemDragHandler.ItemSlotUI.slotIndex, slotIndex);
            }
            else
            {
                inventorySystem.Swap(itemDragHandler.ItemSlotUI.slotIndex, slotIndex);
            }

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            
            if(ItemSlot.item == null) return;

            var toolTip = Tooltip.instance;
            
            if (!toolTip) return;
            
            toolTip.showToolTip.Raise();
            toolTip.text.text = ItemSlot.item.GetToolTipInfo();

        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Item.HideTooltipInfo();
        }
    }
}
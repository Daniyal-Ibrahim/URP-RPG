using UnityEngine.EventSystems;

namespace _Scripts.Inventory_System
{
    public class InventoryItemDragHandler : ItemDragHandler
    {
        public override void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            base.OnPointerUp(eventData);

            if (eventData.hovered.Count == 0)
            {
                // destroy or remove
            }
        }
    }
}
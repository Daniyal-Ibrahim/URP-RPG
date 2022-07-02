using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.Inventory_System
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image itemIcon;
        
        // TODO: assign the slot index when instantiating the obj from the panel
        public int slotIndex;

        private WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.5f);
        
        protected IEnumerator Start()
        {
            yield return delay;
            UpdateSlotUI();
        }
        private void OnEnable()
        {
            UpdateSlotUI();
        }
        public abstract Item SlotItem { get; set; }
        public abstract void UpdateSlotUI();
        public abstract void OnDrop(PointerEventData eventData);
        protected virtual void EnableSlotUI(bool enable)
        {
            itemIcon.enabled = enable;
        }
    }
}

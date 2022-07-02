using System;
using _Scripts.Events.CustomEvents;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Inventory_System
{
    public class ItemDragHandler : MonoBehaviour, IDragHandler,IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] protected ItemEvent onMouseHoverStart;
        [SerializeField] protected VoidEvent onMouseHoverEnd;
        [SerializeField] protected ItemSlotUI itemSlotUI;
        private CanvasGroup _canvasGroup;
        private Transform _originalParent;
        private bool _isHovering;

        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            if (_isHovering)
            {
                //onMouseHoverEnd.Raise();
                _isHovering = false;
            }

        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.position = Input.mousePosition;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                //onMouseHoverEnd.Raise();
                _originalParent = transform.parent;
                transform.SetParent(_originalParent.parent.parent.parent);

                _canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                transform.SetParent(_originalParent);
                transform.localPosition = Vector3.zero;

                _canvasGroup.blocksRaycasts = true;
            }

            if (eventData.hovered.Count == 0)
            {
                // destroy or remove item
            }
        }
        
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            //onMouseHoverStart.Raise(itemSlotUI.SlotItem);
            _isHovering = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            //onMouseHoverEnd.Raise();
            _isHovering = false;
        }


    }
}
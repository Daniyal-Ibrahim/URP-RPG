using System;
using _Scripts.Events.CustomEvents;
using TMPro;
using UnityEngine;

namespace _Scripts.UI_General
{
    public class Tooltip : MonoBehaviour
    {
        public static Tooltip instance;

        public VoidEvent showToolTip;
        public VoidEvent hideToolTip;
        
        public RectTransform toolBackground;
        public TextMeshProUGUI text;
        private RectTransform _rectTransform;
        public Vector2 padding;

        public RectTransform canvasRectTransform;


        public bool showTooltip;
        private void Awake()
        {
            instance = this;
            _rectTransform = GetComponent<RectTransform>();
            //backgroundImage.gameObject.SetActive(false);
        }

        public void ShowToolTip()
        {
            showTooltip = true;
            toolBackground.gameObject.SetActive(true);
        }
        
        public void HideToolTip()
        {
            showTooltip = false;
            toolBackground.gameObject.SetActive(false);
            text.text = string.Empty;
        }

        private void Update()
        {
            if(!showTooltip) return;
            Vector2 position = Input.mousePosition / canvasRectTransform.localScale.x;
            // right
            if (position.x + toolBackground.rect.width > canvasRectTransform.rect.width)
            {
                position.x = canvasRectTransform.rect.width - toolBackground.rect.width;
            }
            if (position.y + toolBackground.rect.height > canvasRectTransform.rect.height)
            {
                position.y = canvasRectTransform.rect.height - toolBackground.rect.height;
            }
            
            _rectTransform.anchoredPosition = position;

            toolBackground.sizeDelta = text.GetRenderedValues(false)+padding;
        }
    }
}
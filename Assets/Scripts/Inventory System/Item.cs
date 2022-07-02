using System;
using _Scripts.Scriptable_Object_Variables;
using _Scripts.UI_General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    //[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Items/Item")]
    public abstract class Item : ScriptableObject
    {
        [PreviewField(75),HideLabel]
        [HorizontalGroup("Split",75)]
        [SerializeField] protected Sprite icon;
        
        [VerticalGroup("Split/Right"),LabelWidth(75)]
        [SerializeField] [TextArea] protected string description;

        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField] protected string id;
        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField] public ColorVariable rarity;
        // max price can be 1 million
        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField,Range(1,1000)] [OnValueChanged("CalculatePrice")] 
        protected int basePrice;
        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField,Range(1,1000)] [OnValueChanged("CalculatePrice")]
        protected int priceMultiplier;
        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField,Range(1,99)] 
        protected int maxStack;
        [BoxGroup("Basic Info")] [LabelWidth(100)]
        [SerializeField] protected int calculatedPrice;
        
        
        // Type - Consumable, Weapon, ? 
        // Rarity - basic, uncommon, rare etc
        // Sell price - base price * modifier
        // Buy price  - base price * modifier

        
        private void CalculatePrice()
        {
            calculatedPrice = basePrice * priceMultiplier;
        }

        public int Price => basePrice * priceMultiplier;
        public int MaxStack
        {
            get => maxStack;
            protected set => maxStack = value;
        }

        public abstract string GetToolTipInfo();

        public static void HideTooltipInfo()
        {
            if(Tooltip.instance)
                Tooltip.instance.hideToolTip.Raise();
        }

        public Sprite Icon => icon;
    }
}
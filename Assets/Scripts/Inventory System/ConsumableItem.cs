using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu(fileName = "New consumable", menuName = "Inventory System/Consumable")]
    public class ConsumableItem : Item
    {
        [BoxGroup("Item Info")] [LabelWidth(100)]
        [SerializeField] private string useText = "What the item does";
        
        public override string GetToolTipInfo()
        {
            var c = ColorUtility.ToHtmlStringRGB(rarity.value);
            var builder = new StringBuilder();
            builder.Append("<size=30>").Append($"<color=#{c}>").Append(name).Append("</color>").AppendLine();
            builder.Append("<size=20>").Append(description).AppendLine();
            builder.Append("<size=20>").Append("<color=green>Use: ").Append(useText).Append("</color>");
            
            return builder.ToString();
        }
        
    }
}
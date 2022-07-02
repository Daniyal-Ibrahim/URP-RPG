using System;
using System.Text;
using _Scripts.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon")]
    public class WeaponItem : Item
    {
        [BoxGroup("Damage Info")] [LabelWidth(100)]
        [SerializeField,Range(1,10)] private int baseDamage;
        [SerializeField,Range(1,10)] private int maxDamage;

        private int _minDmg;
        private int _maxDmg;
        private void Awake()
        {
            MaxStack = 1;
            
        }

        private void OnEnable()
        {
            _minDmg = baseDamage;
            _maxDmg = baseDamage + maxDamage;
        }

        public override string GetToolTipInfo()
        {
            var builder = new StringBuilder();
            builder.Append("<size=30>").Append(name).AppendLine();
            builder.Append("<size=20>").Append(description).AppendLine();
            builder.Append("<size=20>").Append("<color=red>Damage: </color>").Append($"{_minDmg}-{_maxDmg}");
            //builder.Append("<color=green>Use: ").Append(useText).Append("</color>").Append("test").AppendLine();
            
            return builder.ToString();
        }
    }
}
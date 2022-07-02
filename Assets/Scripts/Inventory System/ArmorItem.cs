using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu(fileName = "New Armor", menuName = "Inventory System/Armor")]

    public class ArmorItem : Item
    {
        [SerializeField] private ArmorType armorType;
        
        
        public override string GetToolTipInfo()
        {
            throw new System.NotImplementedException();
        }
        protected enum ArmorType
        {
            Head,Body,Arm,Leg
        }
    }
    
    
}
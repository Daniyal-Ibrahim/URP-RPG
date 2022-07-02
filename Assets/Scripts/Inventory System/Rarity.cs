using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Inventory System/Items/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);
        
        public Color TextColour => textColour;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Inventory_System
{
    [CreateAssetMenu(fileName = "New Database", menuName = "Inventory System/Items/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        public List<DictionaryData> database;
        
        
        
    }


    [Serializable]
    public class DictionaryData
    {
        public int key;
        public Item value;
    }
}
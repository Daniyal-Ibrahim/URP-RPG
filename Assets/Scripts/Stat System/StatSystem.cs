using _Scripts.Events.CustomEvents;
using UnityEngine;

namespace _Scripts.Stat_System
{
    [CreateAssetMenu(fileName = "New Stat Array", menuName = "Stat System/Stat")]
    public class StatSystem : ScriptableObject
    {
        public VoidEvent onResultsCalculated;

        [Header("Test values")]
        [SerializeField, Range(1,20)] private int level;
        
        [SerializeField, Range(1,20)] private int hpDice;
        [SerializeField, Range(1,20)] private int mpDice;
        
        [SerializeField, Range(1,20)] private int dmgPhyDieSize;
        [SerializeField, Range(1,20)] private int dmgPhyDieMultiplier;
        
        [SerializeField, Range(1,20)] private int dmgMagicDieSize;
        [SerializeField, Range(1,20)] private int dmgMagicDieMultiplier;
        
        [SerializeField, Range(6, 20)] private int baseStat;

        [Header("Base Stats")]
        // Constitution : used for Health points
        public int vigor = 10;
        // Strength     : used for Physical damage calculation
        public int might = 10;
        // Dexterity    : used for Physical damage calculation 
        public int reflex = 10;
        // Intelligence : used for Magic points 
        public int mind = 10;
        // Wisdom       : used for Magic damage calculation 
        public int will = 10;
        // Charisma     : used for Magic damage calculation 
        public int sanity = 10;
    
        [Header("Bonus Stats")]
        // Constitution
        public int vigorBonus;
        // Strength
        public int mightBonus;
        // Dexterity
        public int reflexBonus;
        // Intelligence
        public int mindBonus;
        // Wisdom
        public int willBonus;
        // Charisma
        public int sanityBonus;

        [field: Header("Final Stats")]
        public int healthMax;
        public int healthCurrent;
        public int magicMax;
        public int magicCurrent;
    
        [SerializeField] private int physicalDmg;
        [SerializeField] private int magicDmg;

        private void OnEnable()
        {
            CalculateStats();
        }

        [ContextMenu("Calculate Stats")]
        public void CalculateStats()
        {
            CalculateBonus();
        
            CalculateHp();
            CalculateMp();
            CalculateDamage();
            onResultsCalculated.Raise();
        }

        private void CalculateHp()
        {
            healthMax = healthCurrent = level * hpDice + (vigorBonus * level);
        }
        private void CalculateMp()
        {
            // level * die + bonus
            magicMax = magicCurrent = level * mpDice + (mindBonus * level);
        }
        private void CalculateDamage()
        {
            // weapon damage + bonus 
            physicalDmg = (dmgPhyDieMultiplier * dmgPhyDieSize) + mightBonus;
            magicDmg = (dmgMagicDieMultiplier * dmgMagicDieSize) + mindBonus;
        }
        private void CalculateBonus()
        {
            vigorBonus = CalculateBonus(ref vigor);
            mightBonus = CalculateBonus(ref might);
            reflexBonus = CalculateBonus(ref reflex);
            mindBonus = CalculateBonus(ref mind);
            willBonus = CalculateBonus(ref will);
            sanityBonus = CalculateBonus(ref sanity);
        }

        private int CalculateBonus(ref int value)
        {
            var bonus = (value - baseStat)/2;
            return bonus;
        }
    }
}
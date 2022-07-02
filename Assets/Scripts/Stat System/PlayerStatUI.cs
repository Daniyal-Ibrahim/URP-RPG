using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace _Scripts.Stat_System
{
    public class PlayerStatUI : MonoBehaviour
    {
        
        [SerializeField] private StatSystem playerStats;

        [SerializeField] private Image hpBar;
        [SerializeField] private Image mpBar;

        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI mpText;
        
        [SerializeField] private TextMeshProUGUI vigorText;
        [SerializeField] private TextMeshProUGUI mightText;
        [SerializeField] private TextMeshProUGUI reflexText;
        [SerializeField] private TextMeshProUGUI mindText;
        [SerializeField] private TextMeshProUGUI willText;
        [SerializeField] private TextMeshProUGUI sanityText;

        private void Start()
        {
            playerStats.onResultsCalculated.Raise();
            //UpdateStatsUI();
        }

        private void Update()
        {
            // TODO: Move to event
            hpText.text = $"{playerStats.healthCurrent}/{playerStats.healthMax}";
            mpText.text = $"{playerStats.magicCurrent}/{playerStats.magicMax}";
            hpBar.fillAmount = playerStats.healthCurrent / playerStats.healthMax;
            mpBar.fillAmount = playerStats.magicCurrent / playerStats.magicMax;
        }

        public void UpdateStatsUI()
        {
            if (!playerStats) return;
            
            vigorText.text = StatUpdated(playerStats.vigor, playerStats.vigorBonus);
            mightText.text = StatUpdated(playerStats.might, playerStats.mightBonus);
            reflexText.text = StatUpdated(playerStats.reflex, playerStats.reflexBonus);
            mindText.text = StatUpdated(playerStats.mind, playerStats.mindBonus);
            willText.text = StatUpdated(playerStats.will, playerStats.willBonus);
            sanityText.text = StatUpdated(playerStats.sanity, playerStats.sanityBonus);

        }

        private static string StatUpdated(int stat, int bonus)
        {
            var builder = new StringBuilder();
            builder.Append($"{stat} ({bonus})");
            return builder.ToString();
        }
    }
}

using TMPro;
using UnityEngine;

namespace Fantazee.Encounters.Ui.Rewards
{
    public class HealthEncounterReward : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text healthText;
        
        public void Initialize(int health)
        {
            healthText.text = health.ToString();
        }
    }
}
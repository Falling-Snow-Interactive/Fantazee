using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.StatusEffects.Ui
{
    public class BattleStatusEntryUi : MonoBehaviour
    {
        public BattleStatusEffect StatusEffect { get; private set; }

        [SerializeField]
        private TMP_Text amountText;

        [SerializeField]
        private Image icon;

        private void OnEnable()
        {
            if (StatusEffect != null)
            {
                StatusEffect.Changed += OnChanged;
            }
        }

        private void OnDisable()
        {
            if (StatusEffect != null)
            {
                StatusEffect.Changed -= OnChanged;
            }
        }

        public void Initialize(BattleStatusEffect statusEffect)
        {
            this.StatusEffect = statusEffect;
            
            amountText.text = statusEffect.TurnsRemaining.ToString();
            icon.sprite = statusEffect.Data.Icon;
            
            statusEffect.Changed += OnChanged;
        }
        
        private void OnChanged()
        {
            amountText.text = StatusEffect.TurnsRemaining.ToString();
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Characters.Intentions.Ui
{
    public class IntentionEntryUi : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text amount;

        public void Initialize(Intention intention)
        {
            icon.sprite = intention.Information.Icon;
            amount.text = intention.Amount.ToString();
            amount.color = Color.white; // intention.Information.Color;
        }
    }
}
using DG.Tweening;
using ProjectYahtzee.Boons;
using ProjectYahtzee.Boons.Information;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectYahtzee.Battle.Boons.Ui
{
    public class BoonsEntryUi : MonoBehaviour
    {
        private Boon boon;
        
        [Header("References")]

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text nameText;
        
        [SerializeField]
        private TMP_Text descriptionText;

        [FormerlySerializedAs("valueText")]
        [SerializeField]
        private TMP_Text bonusText;
        
        [SerializeField]
        private TMP_Text modText;

        public void Initialize(Boon boon)
        {
            this.boon = boon;

            UpdateUi();
        }

        public void UpdateUi()
        {
            BoonInformation info = boon.Information;
            icon.sprite = info.Icon;
            nameText.text = info.LocName.GetLocalizedString();
            descriptionText.text = info.LocDescription.GetLocalizedString();
            bonusText.text = $"{boon.GetBonusText()}";
        }

        public void Squish()
        {
            Vector3 squish = new Vector3(0.1f, -0.1f, 0);
            transform.DOPunchScale(squish, 0.25f, 10, 1f);
        }

        public void Punch()
        {
            transform.DOPunchPosition(Vector3.right * 50f, 0.25f, 10, 1f);
        }
    }
}

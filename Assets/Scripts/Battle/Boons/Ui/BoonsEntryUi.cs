using DG.Tweening;
using ProjectYahtzee.Boons;
using ProjectYahtzee.Boons.Information;
using TMPro;
using UnityEngine;
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

        [SerializeField]
        private TMP_Text valueText;
        
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
            valueText.text = $"+{boon.GetValue()}";
            modText.text = $"+{boon.GetModifier()}";
        }

        public void Pop()
        {
            transform.DOPunchScale(Vector3.one * 0.1f, 0.25f, 10, 1f);
        }
    }
}

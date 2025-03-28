using DG.Tweening;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public abstract class ShopEntryUi : SimpleButton
    {
        [Header("References")]

        [SerializeField]
        private TMP_Text nameText;
        protected TMP_Text NameText => nameText;
        
        [SerializeField]
        private TMP_Text descriptionText;
        protected TMP_Text DescriptionText => descriptionText;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;
        protected CurrencyEntryUi CurrencyEntry => currencyEntry;

        [SerializeReference]
        private Currency cost;
        public Currency Cost => cost;

        protected void ShowEntry(string name, string desc, Currency currency)
        {
            if (nameText != null)
            {
                nameText.text = name;
            }

            if (descriptionText != null)
            {
                descriptionText.text = desc;
            }

            cost = currency;
            currencyEntry.SetCurrency(currency);
        }

        public abstract void OnEntrySelected();

        public void PlayCantAfford()
        {
            // DOTween.Complete(transform);
            // DOTween.Complete(borderColor);
            //
            // Color b1 = borderColor.color;
            // Color b2 = Color.red;
            // b2.a = b1.a;
            // borderColor.color = b2;
            // borderColor.DOColor(b1, 0.2f);
            //
            // transform.DOPunchScale(Vector3.one * -0.1f, 0.2f, 10, 1f);
            // CurrencyEntry.PlayCantAfford();
        }
    }
}

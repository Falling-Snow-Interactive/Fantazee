using Fantahzee.Currencies;
using Fantahzee.Currencies.Ui;
using TMPro;
using UnityEngine;

namespace Fantahzee.Shop.Ui.Entries
{
    public abstract class ShopEntryUi : MonoBehaviour
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

        protected void ShowEntry(string name, string desc, Currency currency)
        {
            nameText.text = name;
            descriptionText.text = desc;
            currencyEntry.SetCurrency(currency);
        }

        public abstract void OnEntrySelected();
    }
}

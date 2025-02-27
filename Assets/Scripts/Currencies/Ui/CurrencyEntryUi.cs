using Fantazee.Currencies.Information;
using Fantazee.Currencies.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Currencies.Ui
{
    public class CurrencyEntryUi : MonoBehaviour
    {
        [Header("Currency")]

        [SerializeReference]
        private Currency currency;
        
        [Header("References")]

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private TMP_Text text;

        private void OnEnable()
        {
            if (currency != null)
            {
                currency.Changed += UpdateAmount;
            }
        }

        private void OnDisable()
        {
            if (currency != null)
            {
                currency.Changed -= UpdateAmount;
            }
        }

        public void SetCurrency(Currency currency)
        {
            Debug.Log($"CurrencyEntry - Set Currency {currency}");
            this.currency = currency;
            if (CurrencySettings.Settings.CurrencyInformation.TryGetInformation(currency.type, out CurrencyInformation info))
            {
                if (icon)
                {
                    icon.sprite = info.Icon;
                }

                UpdateAmount();
            }
            
            currency.Changed += UpdateAmount;
        }

        private void UpdateAmount()
        {
            Debug.Log($"CurrencyEntry - UpdateAmount {currency}");
            text.text = currency.amount.ToString();
        }
    }
}

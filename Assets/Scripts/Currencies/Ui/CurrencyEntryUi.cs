using Fantahzee.Currencies.Information;
using Fantahzee.Currencies.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantahzee.Currencies.Ui
{
    public class CurrencyEntryUi : MonoBehaviour
    {
        [Header("Currency")]

        [SerializeField]
        private Currency currency;
        
        [Header("Background")]

        [SerializeField]
        private Sprite backgroundSprite;

        [SerializeField]
        private Color backgroundColor;

        [Header("Outline")]

        [SerializeField]
        private Vector2 outlineSize = Vector2.zero;
        
        [SerializeField]
        private Color outlineColor = Color.white;

        [Header("Text")]

        [SerializeField]
        private Color textColor = Color.black;
        
        [Header("References")]
        
        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Outline outline;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private TMP_Text text;

        private void OnValidate()
        {
            if (backgroundImage)
            {
                backgroundImage.sprite = backgroundSprite;
                backgroundImage.color = backgroundColor;
            }

            if (outline)
            {
                outline.effectColor = outlineColor;
                outline.effectDistance = outlineSize;
            }

            if (text)
            {
                text.color = textColor;
            }
        }

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

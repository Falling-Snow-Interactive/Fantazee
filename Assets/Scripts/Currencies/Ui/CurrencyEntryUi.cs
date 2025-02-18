using ProjectYahtzee.Currencies.Information;
using ProjectYahtzee.Currencies.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.Currencies.Ui
{
    public class CurrencyEntryUi : MonoBehaviour
    {
        [Header("Currency")]

        [SerializeField]
        private CurrencyType currencyType;
        
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

            SetCurrency(currencyType);
        }

        private void OnEnable()
        {
            GameController.Instance.GameInstance.Wallet.Changed += OnWalletChanged;
        }

        private void OnDisable()
        {
            GameController.Instance.GameInstance.Wallet.Changed -= OnWalletChanged;
        }

        private void OnWalletChanged()
        {
            UpdateAmount();
        }

        public void SetCurrency(CurrencyType currencyType)
        {
            Debug.Log($"CurrencyEntry - Set Currency {currencyType}");
            this.currencyType = currencyType;
            if (CurrencySettings.Settings.CurrencyInformation.TryGetInformation(currencyType, out CurrencyInformation info))
            {
                if (icon)
                {
                    icon.sprite = info.Icon;
                }

                if (GameController.Instance && GameController.Instance.GameInstance != null)
                {
                    UpdateAmount();
                }
                else
                {
                    text.text = "000";
                }
            }
        }

        private void UpdateAmount()
        {
            if (GameController.Instance.GameInstance.Wallet.TryGetCurrency(currencyType, out Currency currency))
            {
                Debug.Log($"CurrencyEntry - UpdateAmount {currency}");
                text.text = currency.amount.ToString();
            }
        }
    }
}

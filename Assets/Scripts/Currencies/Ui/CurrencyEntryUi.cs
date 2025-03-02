using DG.Tweening;
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
        private Image background;

        [SerializeField]
        private Image border;

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
            text.text = currency.amount.ToString();
        }

        public void PlayCantAfford()
        {
            DOTween.Complete(transform);
            DOTween.Complete(background);
            DOTween.Complete(border);
            DOTween.Complete(text);
            
            transform.DOPunchScale(Vector3.one * -0.1f, 0.2f, 10, 1f);
            Color c1 = background.color;
            Color c2 = Color.red;
            c2.a = c1.a;
            // background.color = c2;
            background.DOColor(c1, 0.2f);
            
            Color b1 = border.color;
            Color b2 = Color.red;
            b2.a = b1.a;
            border.color = b2;
            border.DOColor(b1, 0.2f);

            Color t1 = text.color;
            Color t2 = Color.red;
            t2.a = t1.a;
            text.color = t2;
            text.DOColor(t1, 0.2f);
        }
    }
}

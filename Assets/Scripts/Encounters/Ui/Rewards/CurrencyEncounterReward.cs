using Fantazee.Currencies;
using Fantazee.Currencies.Information;
using Fantazee.Currencies.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Encounters.Ui.Rewards
{
    public class CurrencyEncounterReward : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        
        [SerializeField]
        private TMP_Text amount;
        
        public void Initialize(Currency currency)
        {
            if(CurrencySettings.Settings.CurrencyInformation.TryGetInformation(currency.type, 
                                                                               out CurrencyInformation info))
            {
                image.sprite = info.Icon;
                amount.text = currency.amount.ToString();
            }
        }
    }
}
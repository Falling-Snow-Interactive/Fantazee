using System;
using System.Collections.Generic;
using Fantazhee.Currencies;
using Fantazhee.Currencies.Ui;
using UnityEngine;

namespace Fantazhee.Battle.Ui.WinScreens
{
    public class WinScreen : MonoBehaviour
    {
        private Action onContinue;
        
        private readonly List<CurrencyEntryUi> currencyEntries = new();
        
        [Header("Prefabs")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform currencyContainer;
        
        public void Initialize(BattleRewards rewards, Action onContinue)
        {
            this.onContinue = onContinue;
            
            foreach (CurrencyEntryUi entry in currencyEntries)
            {
                Destroy(entry.gameObject);
            }
            
            currencyEntries.Clear();
            foreach (Currency currency in rewards.Currencies)
            {
                CurrencyEntryUi currencyEntry = Instantiate(currencyEntryPrefab, currencyContainer);
                currencyEntry.SetCurrency(currency);
                
                currencyEntries.Add(currencyEntry);
            }
        }

        public void OnContinueClicked()
        {
            onContinue?.Invoke();
        }
    }
}
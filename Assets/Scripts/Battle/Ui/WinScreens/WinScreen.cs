using System;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui.WinScreens
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

        [SerializeField]
        private Button button;
        
        public void Initialize(BattleRewards rewards, Action onContinue)
        {
            this.onContinue = onContinue;
            
            foreach (CurrencyEntryUi entry in currencyEntries)
            {
                Destroy(entry.gameObject);
            }
            
            currencyEntries.Clear();
            foreach (Currency currency in rewards.Wallet.Currencies)
            {
                CurrencyEntryUi currencyEntry = Instantiate(currencyEntryPrefab, currencyContainer);
                currencyEntry.SetCurrency(currency);
                
                currencyEntries.Add(currencyEntry);
            }
            
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }

        public void OnContinueClicked()
        {
            onContinue?.Invoke();
        }
    }
}
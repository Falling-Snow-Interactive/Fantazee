using System;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.Screens;
using UnityEngine;

namespace Fantazee.Shop.Ui
{
    public class ShopUi : MonoBehaviour
    {
        [Header("Scene References")]
        
        [SerializeField]
        private ShopController shopController;

        [Header("References")]

        [SerializeField]
        private ShopMainScreen mainScreen;
        
        [SerializeField]
        private SpellScoreScreen spellScoreScreen;
        
        [SerializeField]
        private ScoreScoreScreen scoreScoreScreen;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;

        private void Awake()
        {
            mainScreen.Show(true);
            spellScoreScreen.Hide(true);
            scoreScoreScreen.Hide(true);
        }

        public void Initialize(ShopInventory inventory)
        {
            Debug.Log("ShopUi - Initialize");
            if (GameInstance.Current.Character.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency currency))
            {
                currencyEntry.SetCurrency(currency);
            }

            mainScreen.Initialize(inventory, OnSpellSelected, OnRelicSelected, OnScoreSelected);
        }
        
        #region Main screen

        private void OnSpellSelected(SpellEntry spellEntry)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(spellEntry.Data.Cost))
            {
                ShowSpellScreen(spellEntry);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                spellEntry.PlayCantAfford();
            }
        }

        private void OnRelicSelected(RelicEntry relicEntry)
        {
            
        }

        private void OnScoreSelected(ShopScoreEntry shopScoreEntry)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(shopScoreEntry.Score.Information.Cost))
            {
                // ShowScoreScoreScreen(shopScoreEntry);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                // shopScoreEntry.PlayCantAfford();
            }
        }
        
        #endregion
        
        #region Score spell screen

        private void ShowSpellScreen(SpellEntry spellEntry, Action onComplete = null)
        {
            spellScoreScreen.Initialize(spellEntry, OnSpellScreenComplete);
            
            mainScreen.Hide();
            spellScoreScreen.Show();
        }

        private void OnSpellScreenComplete()
        {
            spellScoreScreen.Hide();
            mainScreen.Show();
        }
        
        #endregion
        
        #region Score spell screen
        
        private void ShowScoreScoreScreen(ScoreShopEntry scoreShopEntry, Action onComplete = null)
        {
            scoreScoreScreen.Initialize(scoreShopEntry, OnScoreScoreScreenComplete);
            
            mainScreen.Hide();
            scoreScoreScreen.Show();
        }

        private void OnScoreScoreScreenComplete()
        {
            scoreScoreScreen.Hide();
            mainScreen.Show();
        }
        
        #endregion
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
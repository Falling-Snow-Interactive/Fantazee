using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.Screens;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.Serialization;
using ScoreEntry = Fantazee.Battle.Score.Ui.ScoreEntry;

namespace Fantazee.Shop.Ui
{
    public class ShopUi : MonoBehaviour
    {
        [Header("Scene References")]
        
        [SerializeField]
        private ShopController shopController;
        
        // [SerializeField]
        // private Entry otherEntryPrefab;

        [Header("References")]

        [SerializeField]
        private ShopMainScreen mainScreen;
        
        [SerializeField]
        private SpellScoreScreen spellScoreScreen;
        
        [SerializeField]
        private ScoreScoreScreen scoreScoreScreen;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;

        // [Header("Score Screen References")]
        //
        // [SerializeField]

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

        private void OnScoreSelected(ScoreShopEntry scoreEntry)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(scoreEntry.Information.Cost))
            {
                ShowScoreScoreScreen(scoreEntry);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                scoreEntry.PlayCantAfford();
            }
        }
        
        #endregion
        
        #region Score spell screen

        private void ShowSpellScreen(SpellEntry spellEntry, Action onComplete = null)
        {
            spellScoreScreen.Initialize(spellEntry, OnSpellScreenComplete);
            
            mainScreen.SlideOut();
            spellScoreScreen.SlideIn();
        }

        private void OnSpellScreenComplete()
        {
            spellScoreScreen.SlideOut();
            mainScreen.SlideIn();
        }
        
        #endregion
        
        #region Score spell screen
        
        private void ShowScoreScoreScreen(ScoreShopEntry scoreShopEntry, Action onComplete = null)
        {
            scoreScoreScreen.Initialize(scoreShopEntry, OnScoreScoreScreenComplete);
            
            mainScreen.SlideOut();
            scoreScoreScreen.SlideIn();
        }

        private void OnScoreScoreScreenComplete()
        {
            scoreScoreScreen.SlideOut();
            mainScreen.SlideIn();
        }
        
        #endregion
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
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
using ScoreEntry = Fantazee.Scores.Ui.ScoreEntry;

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
        
        [FormerlySerializedAs("shopScoreScreen")]
        [SerializeField]
        private ShopSpellScreen spellScreen;
        
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

            mainScreen.Initialize(inventory.Spells, OnSpellSelected);
        }
        
        #region Main screen

        private void OnSpellSelected(SpellEntry spellEntry)
        {
            ShowSpellScoreScreen(spellEntry);
        }

        private void OnRelicSelected(RelicEntry relicEntry)
        {
            
        }

        private void OnScoreSelected(ScoreEntry scoreEntry)
        {
            
        }
        
        #endregion
        
        #region Score spell screen

        private void ShowSpellScoreScreen(SpellEntry spellEntry, Action onComplete = null)
        {
            spellScreen.Initialize(spellEntry, OnSpellScreenComplete);
            
            mainScreen.SlideOut();
            spellScreen.SlideIn();
            
            spellEntry.transform.DOMove(spellScreen.transform.position, 0.75f);
        }

        private void OnSpellScreenComplete()
        {
            spellScreen.SlideOut();
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
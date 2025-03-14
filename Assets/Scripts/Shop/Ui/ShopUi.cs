using System;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.Screens;
using FMOD.Studio;
using FMODUnity;
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
        
        // Audio
        private EventInstance swooshSfx;

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

            swooshSfx = RuntimeManager.CreateInstance(ShopSettings.Settings.SwooshSfx);

            mainScreen.Initialize(inventory, OnSpellSelected, OnRelicSelected, OnScoreSelected);
        }
        
        #region Main screen

        private void OnSpellSelected(SpellEntry spellEntry)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(spellEntry.Cost))
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
            if (GameInstance.Current.Character.Wallet.CanAfford(relicEntry.Cost))
            {
                ShopController.Instance.MakePurchase(relicEntry.Cost);
                GameInstance.Current.Character.AddRelic(relicEntry.Relic);
                Destroy(relicEntry.gameObject);
            }
            else
            {
                relicEntry.PlayCantAfford();
            }
        }

        private void OnScoreSelected(ShopScoreButtonPurchase purchaseScoreButton)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(purchaseScoreButton.Score.Data.Cost))
            {
                ShowScoreScoreScreen(purchaseScoreButton);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                purchaseScoreButton.PlayCantAfford();
            }
        }
        
        #endregion
        
        #region Score spell screen

        private void ShowSpellScreen(SpellEntry purchase, Action onComplete = null)
        {
            spellScoreScreen.Initialize(purchase, OnSpellScreenComplete);
            
            mainScreen.Hide();
            spellScoreScreen.Show();

            swooshSfx.start();
        }

        private void OnSpellScreenComplete()
        {
            spellScoreScreen.Hide();
            mainScreen.Show();
            
            swooshSfx.start();
        }
        
        #endregion
        
        #region Score spell screen
        
        private void ShowScoreScoreScreen(ShopScoreButtonPurchase scoreShopButton, Action onComplete = null)
        {
            scoreScoreScreen.Initialize(scoreShopButton, OnScoreScoreScreenComplete);
            
            mainScreen.Hide();
            scoreScoreScreen.Show();
            
            swooshSfx.start();
        }

        private void OnScoreScoreScreenComplete()
        {
            scoreScoreScreen.Hide();
            mainScreen.Show();
            
            swooshSfx.start();
        }
        
        #endregion
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
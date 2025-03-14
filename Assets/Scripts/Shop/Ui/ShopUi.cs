using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fantazee.Scores.Scoresheets.Ui;
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
        private ScoresheetUpgradeScreen scoresheetUpgradeScreen;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;
        
        // Audio
        private EventInstance swooshSfx;

        private void Awake()
        {
            mainScreen.Show(true);
            scoresheetUpgradeScreen.gameObject.SetActive(false);
        }

        public void Initialize(ShopInventory inventory)
        {
            if (GameInstance.Current.Character.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency currency))
            {
                currencyEntry.SetCurrency(currency);
            }

            swooshSfx = RuntimeManager.CreateInstance(ShopSettings.Settings.SwooshSfx);
            mainScreen.Initialize(inventory, OnSpellSelected, OnRelicSelected, OnScoreSelected);
        }
        
        #region Main screen

        private void OnSpellSelected(ShopSpellButton spellButton)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(spellButton.Spell.Data.Cost))
            {
                SelectSpell(spellButton);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                spellButton.PlayCantAfford();
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

        private void OnScoreSelected(ShopScoreButton shopScoreButton)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(shopScoreButton.Score.Data.Cost))
            {
                SelectScore(shopScoreButton);
            }
            else
            {
                currencyEntry.PlayCantAfford();
                shopScoreButton.PlayCantAfford();
            }
        }
        
        #endregion

        private void SelectScore(ShopScoreButton shopScoreButton)
        {
            mainScreen.Hide();
            scoresheetUpgradeScreen.gameObject.SetActive(true);
            scoresheetUpgradeScreen.StartScoreUpgrade(shopScoreButton.Score, () =>
                                                                             {
                                                                                 shopScoreButton.gameObject
                                                                                     .SetActive(false);
                                                                                 scoresheetUpgradeScreen.gameObject.SetActive(false);
                                                                                 mainScreen.Show();
                                                                             });
        }

        private void SelectSpell(ShopSpellButton spellButton)
        {
            mainScreen.Hide();
            scoresheetUpgradeScreen.gameObject.SetActive(true);
            scoresheetUpgradeScreen.StartSpellUpgrade(spellButton.Spell, () =>
                                                                             {
                                                                                 spellButton.gameObject
                                                                                     .SetActive(false);
                                                                                 scoresheetUpgradeScreen.gameObject.SetActive(false);
                                                                                 mainScreen.Show();
                                                                             });
        }
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
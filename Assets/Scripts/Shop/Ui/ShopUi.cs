using System;
using DG.Tweening;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fantazee.Scores.Scoresheets.Ui;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.Screens;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.InputSystem;

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
        [Header("Audio")]

        [SerializeField]
        private EventReference swooshSfxRef;
        private EventInstance? swooshSfx;
        
        [SerializeField]
        private EventReference purchaseSfxRef;
        private EventInstance? purchaseSfx;

        [Header("     Input")]

        [SerializeField]
        private InputActionReference cancelActionRef;
        private InputAction cancelAction;

        private void Awake()
        {
            mainScreen.Show(true);
            scoresheetUpgradeScreen.Hide(true);

            if (!swooshSfxRef.IsNull)
            {
                swooshSfx = RuntimeManager.CreateInstance(swooshSfxRef);
            }

            if (!purchaseSfxRef.IsNull)
            {
                purchaseSfx = RuntimeManager.CreateInstance(purchaseSfxRef);
            }

            cancelAction = cancelActionRef.ToInputAction();
        }

        private void OnEnable()
        {
            cancelAction.performed += OnCancelAction;
            
            cancelAction.Enable();
        }

        private void OnDisable()
        {
            cancelAction.performed -= OnCancelAction;
            
            cancelAction.Disable();
        }

        public void Initialize(ShopInventory inventory)
        {
            if (GameInstance.Current.Character.Wallet.TryGetCurrency(CurrencyType.currency_00_gold, out Currency currency))
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

        private void OnRelicSelected(RelicShopEntry relicShopEntry)
        {
            if (GameInstance.Current.Character.Wallet.CanAfford(relicShopEntry.Relic.Data.Cost))
            {
                ShopController.Instance.MakePurchase(relicShopEntry.Relic.Data.Cost);
                GameInstance.Current.Character.AddRelic(relicShopEntry.Relic);
                Destroy(relicShopEntry.gameObject);
                mainScreen.SelectFirstButton();
            }
            else
            {
                // relicShopEntry.PlayCantAfford();
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
            scoresheetUpgradeScreen.Show();
            scoresheetUpgradeScreen.StartScoreUpgrade(shopScoreButton.Score, () =>
                                                                             {
                                                                                 UpgradeFinished(shopScoreButton.Score.Data.Cost,
                                                                                     shopScoreButton.gameObject);
                                                                             }, UpgradeCanceled);
        }

        private void SelectSpell(ShopSpellButton spellButton)
        {
            mainScreen.Hide();
            scoresheetUpgradeScreen.Show();
            scoresheetUpgradeScreen.StartSpellUpgrade(spellButton.Spell, () =>
                                                                             {
                                                                                 UpgradeFinished(spellButton.Spell.Data.Cost, 
                                                                                     spellButton.gameObject);
                                                                             },UpgradeCanceled);
        }

        private void UpgradeCanceled()
        {
            scoresheetUpgradeScreen.Hide();
            mainScreen.Show();
        }

        private void UpgradeFinished(Currency cost, GameObject objectToDisable)
        {
            MakePurchase(cost);
            // objectToDisable.SetActive(false);

            if (objectToDisable.TryGetComponent(out ShopSpellButton spellButton))
            {
                mainScreen.SpellEntries.Remove(spellButton);
            }
            else if (objectToDisable.TryGetComponent(out ShopScoreButton scoreButton))
            {
                mainScreen.ScorePurchaseEntries.Remove(scoreButton);
            }
            
            DOTween.Complete(objectToDisable);
            Destroy(objectToDisable);
            scoresheetUpgradeScreen.Hide();
            mainScreen.Show();
        }

        private void MakePurchase(Currency currency)
        {
            GameInstance.Current.Character.Wallet.Remove(currency);
            purchaseSfx?.start();
        }
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
        
        private void OnCancelAction(InputAction.CallbackContext callbackContext)
        {
            if (mainScreen.IsActiveScreen)
            {
                OnLeaveButtonClicked();
            }
            else
            {
                scoresheetUpgradeScreen.OnCancelSelect();
            }
        }
    }
}
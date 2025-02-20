using System;
using System.Collections.Generic;
using Fantazee.Boons;
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Boons.Settings;
using UnityEngine;

namespace Fantazee.Shop.Ui
{
    public class ShopUi : MonoBehaviour
    {
        [Header("Scene References")]
        
        [SerializeField]
        private ShopController shopController;
        
        [Header("Prefabs")]
        
        [SerializeField]
        private BoonEntry boonEntryPrefab;
        
        [SerializeField]
        private RelicEntry relicEntryPrefab;

        private List<BoonEntry> boonEntries = new();
        private List<RelicEntry> relicEntries = new();
        
        // [SerializeField]
        // private Entry otherEntryPrefab;

        [Header("Ui References")]

        [SerializeField]
        private Transform boonContent;
        
        [SerializeField]
        private Transform relicContent;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;

        public void Initialize(ShopInventory inventory)
        {
            Debug.Log("ShopUi - Initialize");
            if (GameController.Instance.GameInstance.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency currency))
            {
                currencyEntry.SetCurrency(currency);
            }

            foreach (BoonType boon in inventory.Boons)
            {
                BoonEntry boonEntry = Instantiate(boonEntryPrefab, boonContent);
                boonEntry.Initialize(boon, OnBoonSelected);
            }
        }

        private void OnBoonSelected(BoonEntry boonEntry)
        {
            Debug.Log($"ShopUi - OnBoonSelected {boonEntry.Boon}");

            if (shopController.TryPurchase(boonEntry.Boon))
            {
                boonEntries.Remove(boonEntry);
                Destroy(boonEntry.gameObject);
            }
        }
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
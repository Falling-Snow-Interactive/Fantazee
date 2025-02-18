using ProjectYahtzee.Currencies;
using ProjectYahtzee.Currencies.Ui;
using UnityEngine;

namespace ProjectYahtzee.Shop.Ui
{
    public class ShopUi : MonoBehaviour
    {
        [Header("Scene References")]
        
        [SerializeField]
        private ShopController shopController;

        [Header("Ui References")]

        [SerializeField]
        private Transform content;
        
        [SerializeField]
        private CurrencyEntryUi currencyEntry;

        public void Initialize()
        {
            Debug.Log("ShopUi - Initialize");
            currencyEntry.SetCurrency(CurrencyType.Gold);
        }
        
        public void OnLeaveButtonClicked()
        {
            Debug.Log("ShopUi - Leave button clicked");
            shopController.LeaveShop();
        }
    }
}
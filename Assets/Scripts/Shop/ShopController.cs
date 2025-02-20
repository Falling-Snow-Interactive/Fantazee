using Fantazee.Boons;
using Fantazee.Boons.Information;
using Fantazee.Boons.Settings;
using Fantazee.Currencies;
using Fantazee.Shop.Ui;
using UnityEngine;

namespace Fantazee.Shop
{
    public class ShopController : MonoBehaviour
    {
        [Header("Inventory")]

        [SerializeField]
        private ShopInventory inventory;
        
        [Header("Scene References")]
        
        [SerializeField]
        private ShopUi shopUi;

        private void Start()
        {
            Debug.Log("Shop - Start");
            
            // TODO - Generate shop inventory randomly - KD
            shopUi.Initialize(inventory);
            
            GameController.Instance.ShopReady();
        }

        public bool TryPurchase(BoonType type)
        {
            if (BoonSettings.Settings.Information.TryGetInformation(type, out BoonInformation information))
            {
                Currency cost = information.Cost;
                if (GameController.Instance.GameInstance.Wallet.CanAfford(cost))
                {
                    Boon boon = BoonFactory.Create(type);
                    GameController.Instance.GameInstance.Boons.Add(boon);
                    GameController.Instance.GameInstance.Wallet.Remove(cost);
                    return true;
                }
            }

            return false;
        }

        public void LeaveShop()
        {
            Debug.Log("Shop - Leave shop");
            
            GameController.Instance.ExitShop();
        }
    }
}
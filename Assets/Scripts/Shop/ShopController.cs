using Fantazee.Currencies;
using Fantazee.Instance;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.Shop
{
    public class ShopController : MbSingleton<ShopController>
    {
        [Header("Inventory")]

        [SerializeField]
        private ShopData shopData;

        [SerializeReference]
        private ShopInventory inventory;
        
        [Header("Scene References")]
        
        [SerializeField]
        private ShopUi shopUi;
        
        // Audio
        private EventInstance purchaseSfx;

        protected override void Awake()
        {
            base.Awake();

            purchaseSfx = RuntimeManager.CreateInstance(ShopSettings.Settings.PurchaseSfx);
        }

        private void Start()
        {
            Debug.Log("Shop - Start");
            shopUi.Initialize(shopData.GetInventory());
            
            RuntimeManager.PlayOneShot(ShopSettings.Settings.EnterSfx);
            GameController.Instance.ShopReady();
        }

        public void LeaveShop()
        {
            Debug.Log("Shop - Leave shop");
            
            RuntimeManager.PlayOneShot(ShopSettings.Settings.ExitSfx);
            GameController.Instance.ExitShop();
        }

        public bool MakePurchase(Currency cost)
        {
            if (!GameInstance.Current.Character.Wallet.Remove(cost))
            {
                Debug.LogWarning("Shop: Cannot afford spell. Purchase not made.");
                return false;
            }
            
            purchaseSfx.start();
            return true;
        }
    }
}
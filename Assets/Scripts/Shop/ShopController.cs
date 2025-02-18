using ProjectYahtzee.Shop.Ui;
using UnityEngine;

namespace ProjectYahtzee.Shop
{
    public class ShopController : MonoBehaviour
    { 
        [Header("Scene References")]
        
        [SerializeField]
        private ShopUi shopUi;

        private void Start()
        {
            Debug.Log("Shop - Start");
        }

        public void LeaveShop()
        {
            Debug.Log("Shop - Leave shop");
            
            ProjectSceneManager.Instance.LoadMap();
        }
    }
}
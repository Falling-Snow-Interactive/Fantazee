using Fantazee.Shop.Buckets.Scores;
using Fantazee.Shop.Buckets.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [CreateAssetMenu(menuName = "Shop/Data")]
    public class ShopData : ScriptableObject
    {
        [SerializeField]
        private ShopSpellBucket spells = new(); 
        public ShopSpellBucket Spells => spells;
        
        [SerializeField]
        private ShopScoresBucket scores = new();
        public ShopScoresBucket Scores => scores;

        public ShopInventory GetInventory()
        {
            ShopInventory inventory = new(spells.GetRandom(4), scores.GetRandom(2));
            Debug.Log($"Shop - Inventory Generated\n{inventory}");
            return inventory;
        }
    }
}

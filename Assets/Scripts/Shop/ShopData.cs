using System.Collections.Generic;
using Fantazee.Shop.Buckets.Scores;
using Fantazee.Shop.Buckets.Spells;
using Fantazee.Shop.Items;
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
            List<SpellShopItem> spells = this.spells.GetRandom(4);
            List<ScoreShopItem> scores = this.scores.GetRandom(2);
            ShopInventory inventory = new(spells, scores);
            Debug.Log($"Shop - Inventory Generated\n{inventory}");
            return inventory;
        }
    }
}

using System.Collections.Generic;
using Fantazee.Shop.Buckets.Relics;
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
        
        [SerializeField]
        private ShopScoresBucket scores = new();

        [SerializeField]
        private ShopRelicsBucket relics = new();

        public ShopInventory GetInventory()
        {
            List<SpellShopItem> spells = this.spells.GetRandom(10);
            List<ScoreShopItem> scores = this.scores.GetRandom(10);
            List<RelicShopItem> relics = this.relics.GetRandom(10, false);
            ShopInventory inventory = new(spells, scores, relics);
            Debug.Log($"Shop - Inventory Generated\n{inventory}");
            return inventory;
        }
    }
}

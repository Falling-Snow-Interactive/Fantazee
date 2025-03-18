using System.Collections.Generic;
using Fantazee.Relics.Data;
using Fantazee.Shop.Buckets.Relics;
using Fantazee.Shop.Buckets.Scores;
using Fantazee.Shop.Buckets.Spells;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [CreateAssetMenu(menuName = "Shop/Data")]
    public class ShopData : ScriptableObject
    {
        [SerializeField]
        private ShopSpellBucket spells; 
        
        [SerializeField]
        private ShopScoresBucket scores;

        [SerializeField]
        private ShopRelicsBucket relics;

        public ShopInventory GetInventory()
        {
            List<SpellData> spells = this.spells.GetRandom(100);
            List<ScoreShopItem> scores = this.scores.GetRandom(100);
            List<RelicData> relics = this.relics.GetRandom(100, false);
            ShopInventory inventory = new(spells, scores, relics);
            Debug.Log($"Shop: Inventory Generated\n{inventory}");
            return inventory;
        }
    }
}

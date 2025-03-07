using System;
using Fantazee.Currencies;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class ScoreShopItem : ShopItem<PlayerScoreData>
    {
        [SerializeField]
        private PlayerScoreData data;
        public override PlayerScoreData Item => data;
        
        public ScoreShopItem(Currency cost) : base(cost)
        {
        }

        public override string ToString()
        {
            return $"{data} | {Cost}";
        }
    }
}
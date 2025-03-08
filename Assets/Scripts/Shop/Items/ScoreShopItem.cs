using System;
using Fantazee.Characters;
using Fantazee.Currencies;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class ScoreShopItem : ShopItem<CharacterScoreData>
    {
        [SerializeField]
        private CharacterScoreData data;
        public override CharacterScoreData Item => data;
        
        public ScoreShopItem(Currency cost) : base(cost)
        {
        }

        public override string ToString()
        {
            return $"{data} | {Cost}";
        }
    }
}
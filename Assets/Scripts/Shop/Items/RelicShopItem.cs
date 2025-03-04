using System;
using Fantazee.Currencies;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class RelicShopItem : ShopItem<RelicData>
    {
        [SerializeField]
        private RelicData data;
        public override RelicData Item => data;
        
        public RelicShopItem(Currency cost) : base(cost)
        {
        }
    }
}
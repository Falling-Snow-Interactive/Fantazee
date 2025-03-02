using System;
using Fantazee.Currencies;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class SpellShopItem : ShopItem<SpellType>
    {
        [SerializeField]
        private SpellType item;
        public override SpellType Item => item;

        public SpellShopItem(SpellType spell, Currency cost) : base(cost)
        {
        }
    }
}
using System;
using Fantazee.Currencies;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class SpellShopItem : ShopItem<SpellData>
    {
        [SerializeField]
        private SpellData item;
        public override SpellData Item => item;

        public SpellShopItem(SpellData spell, Currency cost) : base(cost)
        {
        }
    }
}
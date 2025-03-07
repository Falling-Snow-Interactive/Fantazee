using System;
using Fantazee.Currencies;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public class SpellShopItem : ShopItem<SpellInstance>
    {
        [SerializeReference]
        private SpellInstance item;
        public override SpellInstance Item => item;

        public SpellShopItem(SpellInstance spell, Currency cost) : base(cost)
        {
        }
    }
}
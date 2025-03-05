using System;
using System.Collections.Generic;
using Fantazee.Shop.Items;
using Fantazee.Spells;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Shop.Buckets.Spells
{
    [CreateAssetMenu(fileName = "ShopSpellBucket", menuName = "Shop/Spell Bucket")]
    public class ShopSpellBucket : Bucket<ShopSpellBucketEntry, SpellShopItem>
    {
        [SerializeField]
        private List<ShopSpellBucketEntry> spells;
        public override List<ShopSpellBucketEntry> Entries => spells;
    }
}
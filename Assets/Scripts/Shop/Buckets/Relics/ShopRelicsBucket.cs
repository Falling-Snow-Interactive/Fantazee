using System.Collections.Generic;
using Fantazee.Relics.Data;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Shop.Buckets.Relics
{
    [CreateAssetMenu(fileName = "ShopRelicBucket", menuName = "Shop/Relic Bucket")]
    public class ShopRelicsBucket : Bucket<ShopRelicsBucketEntry, RelicData>
    {
        [SerializeField]
        private List<ShopRelicsBucketEntry> entries = new List<ShopRelicsBucketEntry>();
        public override List<ShopRelicsBucketEntry> Entries => entries;
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Relics.Data;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Relics.Bucket
{
    [Serializable]
    public class RelicBucket : Bucket<RelicBucketEntry, RelicData>
    {
        [Header("Relics")]
        
        [SerializeField]
        private List<RelicBucketEntry> entries = new();
        public override List<RelicBucketEntry> Entries => entries;
    }
}
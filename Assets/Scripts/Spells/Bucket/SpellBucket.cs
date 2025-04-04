using System.Collections.Generic;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Spells.Bucket
{
    [CreateAssetMenu(menuName = "Spells/Bucket")]
    public class SpellBucket : Bucket<SpellBucketEntry, SpellData>
    {
        [Header("Spells")]

        [SerializeField]
        private List<SpellBucketEntry> entries = new();
        public override List<SpellBucketEntry> Entries => entries;
    }
}
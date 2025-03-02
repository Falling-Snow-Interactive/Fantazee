using System;
using System.Collections.Generic;
using Fantazee.Scores;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Shop.Buckets.Scores
{
    [CreateAssetMenu(fileName = "Score Bucket", menuName = "Shop/Score Bucket")]
    public class ShopScoresBucket : Bucket<ShopScoresBucketEntry, ScoreData>
    {
        [SerializeField]
        private List<ShopScoresBucketEntry> scores = new();
        public override List<ShopScoresBucketEntry> Entries => scores;
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Scores;
using Fantazee.Shop.Items;
using Fantazee.Shop.Ui.Entries;
using Fsi.Gameplay.Buckets;
using UnityEngine;

namespace Fantazee.Shop.Buckets.Scores
{
    [CreateAssetMenu(fileName = "Score Bucket", menuName = "Shop/Score Bucket")]
    public class ShopScoresBucket : Bucket<ShopScoresBucketEntry, ScoreShopItem>
    {
        [SerializeField]
        private List<ShopScoresBucketEntry> scores = new();
        public override List<ShopScoresBucketEntry> Entries => scores;
    }
}
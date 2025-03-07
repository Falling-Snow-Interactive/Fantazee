using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Full House")]
    public class FullHouseScoreData : ScoreData
    {
        public override Dictionary<string, string> GetDescArgs()
        {
            return new Dictionary<string, string>();
        }
    }
}
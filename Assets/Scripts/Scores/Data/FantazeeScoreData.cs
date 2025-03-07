using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Fantazee")]
    public class FantazeeScoreData : ScoreData
    {
        public override Dictionary<string, string> GetDescArgs()
        {
            return new Dictionary<string, string>();
        }
    }
}
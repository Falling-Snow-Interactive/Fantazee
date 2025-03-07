using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Fantazee")]
    public class FantazeeScoreData : ScoreData
    {
        public override ScoreType Type => ScoreType.Fantazee;
    }
}
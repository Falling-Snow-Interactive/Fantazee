using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Chance")]
    public class ChanceScoreData : ScoreData
    {
        public override ScoreType Type => ScoreType.Chance;
    }
}
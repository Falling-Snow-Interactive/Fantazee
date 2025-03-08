using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Two Pairs")]
    public class TwoPairScoreData : ScoreData
    {
        public override ScoreType Type => ScoreType.TwoPair;
    }
}
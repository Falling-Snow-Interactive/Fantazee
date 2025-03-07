using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Full House")]
    public class FullHouseScoreData : ScoreData
    {
        public override ScoreType Type => ScoreType.FullHouse;
    }
}
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/EvenOdd")]
    public class EvenOddScoreData : ScoreData
    {
        public override ScoreType Type => even ? ScoreType.Evens : ScoreType.Odds;

        [Header("Even/Odd")]

        [SerializeField]
        private bool even = true;
    }
}
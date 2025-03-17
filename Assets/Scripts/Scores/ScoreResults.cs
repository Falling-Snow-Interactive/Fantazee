using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Instance;

namespace Fantazee.Scores
{
    public class ScoreResults
    {
        public int Value { get; set; }
        public ScoreInstance Score { get; set; }
        public List<Die> Dice { get; set; }

        public ScoreResults(ScoreInstance score, List<Die> dice)
        {
            Score = score;
            Value = score.Calculate(dice);
            Dice = new List<Die>(dice);
        }
    }
}
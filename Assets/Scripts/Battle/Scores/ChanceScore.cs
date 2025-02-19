using System.Collections.Generic;
using Fantahzee.Dice;
using Fantahzee.Items.Dice;

namespace Fantahzee.Battle.Scores
{
    public class ChanceScore : Score
    {
        public override ScoreType Type => ScoreType.Chance;
        public override int Calculate(List<Die> dice)
        {
            int score = 0;
            foreach (var d in dice)
            {
                score += d.Value;
            }

            return score;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return new List<Die>(dice);
        }
    }
}
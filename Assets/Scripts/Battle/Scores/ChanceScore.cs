using System.Collections.Generic;
using Fantazhee.Items.Dice;
using Fantazhee.Dice;

namespace Fantazhee.Battle.Scores
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
using System.Collections.Generic;

namespace ProjectYahtzee.Battle.Scores
{
    public class ChanceScore : Score
    {
        public override ScoreType Type => ScoreType.Chance;
        public override int Calculate(List<Dice.Die> dice)
        {
            int score = 0;
            foreach (var d in dice)
            {
                score += d.Value;
            }

            return score;
        }

        public override List<Dice.Die> GetScoredDice(List<Dice.Die> dice)
        {
            return new List<Dice.Die>(dice);
        }
    }
}
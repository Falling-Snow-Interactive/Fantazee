using System.Collections.Generic;

namespace ProjectYahtzee.Battle.Scores
{
    public class ChanceScore : Score
    {
        public override ScoreType Type => ScoreType.Chance;
        public override int Calculate(List<Dices.Dice> dice)
        {
            int score = 0;
            foreach (var d in dice)
            {
                score += d.Value;
            }

            return score;
        }

        public override List<Dices.Dice> GetScoredDice(List<Dices.Dice> dice)
        {
            return new List<Dices.Dice>(dice);
        }
    }
}
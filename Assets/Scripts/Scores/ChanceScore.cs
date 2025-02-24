using System;
using System.Collections.Generic;
using Fantazee.Dice;

namespace Fantazee.Scores
{
    [Serializable]
    public class ChanceScore : Score
    {
        public override int Calculate(List<Die> dice)
        {
            int score = 0;
            foreach (Die d in dice)
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
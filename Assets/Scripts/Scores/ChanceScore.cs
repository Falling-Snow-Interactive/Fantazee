using System;
using System.Collections.Generic;
using Fantazee.Dice;

namespace Fantazee.Scores
{
    [Serializable]
    public class ChanceScore : Score
    {
        public override int Calculate()
        {
            int score = 0;
            foreach (Die d in Dice)
            {
                score += d.Value;
            }

            return score;
        }

        public override List<Die> GetScoredDice()
        {
            return new List<Die>(Dice);
        }
    }
}
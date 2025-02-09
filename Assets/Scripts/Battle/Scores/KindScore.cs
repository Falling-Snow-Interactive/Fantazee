using System;
using System.Collections.Generic;

namespace ProjectYahtzee.Battle.Scores
{
    public class KindScore : Score
    {
        public override ScoreType Type { get; }
        private int matches;

        public KindScore(int matches)
        {
            this.matches = matches;
            Type = matches switch
                   {
                       3 => ScoreType.ThreeOfAKind,
                       4 => ScoreType.FourOfAKind,
                       5 => ScoreType.Yahtzee,
                       _ => throw new ArgumentOutOfRangeException(nameof(matches), matches, null)
                   };
        }
        
        public override int Calculate(List<Dices.Dice> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);

            int score = 0;
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                if (kvp.Value >= matches
                    && kvp.Value * kvp.Key > score)
                {
                    score = kvp.Value * kvp.Key;
                }
            }
            
            return score;
        }
    }
}
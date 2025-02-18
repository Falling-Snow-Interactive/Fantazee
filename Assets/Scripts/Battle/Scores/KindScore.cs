using System;
using System.Collections.Generic;
using ProjectYahtzee.Dice;
using ProjectYahtzee.Items.Dice;

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
        
        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            bool isValid = false;
            int total = 0;

            foreach (KeyValuePair<int, int> kvp in dict)
            {
                total += kvp.Key * kvp.Value;
                if (kvp.Value >= matches)
                {
                    isValid = true;
                }
            }

            if (isValid)
            {
                return Type == ScoreType.Yahtzee ? 50 : total;
            }

            return 0;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            return Calculate(dice) > 0 ? new List<Die>(dice) : new List<Die>();
        }
    }
}
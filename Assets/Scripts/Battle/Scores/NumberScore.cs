using System;
using System.Collections.Generic;

namespace ProjectYahtzee.Battle.Scores
{
    [Serializable]
    public class NumberScore : Score
    {
        public override ScoreType Type { get; }
        private int value;

        public NumberScore(int value)
        {
            this.value = value;
            Type = value switch
                   {
                       1 => ScoreType.Ones,
                       2 => ScoreType.Twos,
                       3 => ScoreType.Threes,
                       4 => ScoreType.Fours,
                       5 => ScoreType.Fives,
                       6 => ScoreType.Sixes,
                       _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
                   };
        }
        
        public override int Calculate(List<Dices.Dice> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            
            int value = Type switch
                   {
                       ScoreType.Ones => 1,
                       ScoreType.Twos => 2,
                       ScoreType.Threes => 3,
                       ScoreType.Fours => 4,
                       ScoreType.Fives => 5,
                       ScoreType.Sixes => 6,
                       _ => throw new ArgumentOutOfRangeException()
                   };

            if (dict.TryGetValue(value, out int result))
            {
                return result * value;
            }

            return 0;
        }

        public override List<Dices.Dice> GetScoredDice(List<Dices.Dice> dice)
        {
            List<Dices.Dice> scored = new List<Dices.Dice>();
            foreach (var d in dice)
            {
                if (d.Value == value)
                {
                    scored.Add(d);
                }
            }

            return scored;
        }
    }
}
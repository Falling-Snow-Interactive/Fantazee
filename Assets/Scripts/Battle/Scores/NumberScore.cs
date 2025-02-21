using System;
using System.Collections.Generic;
using Fantazee.Dice;

namespace Fantazee.Battle.Scores
{
    [Serializable]
    public class NumberScore : Score
    {
        public override int Calculate()
        {
            Dictionary<int, int> dict = DiceToDict(Dice);
            
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

        private int GetValue()
        {
            return Type switch
                   {
                       ScoreType.Ones => 1,
                       ScoreType.Twos => 2,
                       ScoreType.Threes => 3,
                       ScoreType.Fours => 4,
                       ScoreType.Fives => 5,
                       ScoreType.Sixes => 6,
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }

        public override List<Die> GetScoredDice()
        {
            List<Die> scored = new();
            foreach (Die d in Dice)
            {
                if (d.Value == GetValue())
                {
                    scored.Add(d);
                }
            }

            return scored;
        }
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    [Serializable]
    public class NumberScore : Score
    {
        public NumberScore(SpellType spell, int number) : base(GetType(number), spell)
        {
        }

        public static ScoreType GetType(int number)
        {
            return number switch
                   {
                       1 => ScoreType.Ones,
                       2 => ScoreType.Twos,
                       3 => ScoreType.Threes,
                       4 => ScoreType.Fours,
                       5 => ScoreType.Fives,
                       6 => ScoreType.Sixes,
                       _ => throw new ArgumentOutOfRangeException(nameof(number), number, null)
                   };
        }
        
        public override int Calculate(List<Die> dice)
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

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            List<Die> scored = new();
            foreach (Die d in dice)
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
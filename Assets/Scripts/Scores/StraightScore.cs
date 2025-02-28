using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Spells;

namespace Fantazee.Scores
{
    [Serializable]
    public class StraightScore : Score
    {
        public StraightScore(List<SpellType> spells, int straight) : base(GetType(straight), spells)
        {
        }

        private static ScoreType GetType(int straight)
        {
            return straight switch
                   {
                       3 => ScoreType.SmallStraight,
                       4 => ScoreType.LargeStraight,
                       _ => throw new ArgumentOutOfRangeException(nameof(straight), straight, null)
                   };
        }
        
        private int GetLength()
        {
            return Type switch
                   {
                       ScoreType.SmallStraight => 3,
                       ScoreType.LargeStraight => 4,
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
        
        public override int Calculate(List<Die> dice)
        {
            // This works with 1,2,3,4,6 but not 1, 3,4,5,6
            Dictionary<int, int> dict = DiceToDict(dice);
            
            int startLimit = 7 - GetLength();
            
            for (int i = 1; i <= startLimit; i++)
            {
                if (dict.TryGetValue(i, out _))
                {
                    int s = i;
                    int count = 1;
                    for (int j = i + 1; j <= 6; j++)
                    {
                        if (dict.TryGetValue(j, out _))
                        {
                            s += j;
                            count++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (count >= GetLength())
                    {
                        return Type == ScoreType.SmallStraight ? 10 : 15;
                    }
                }
            }

            return 0;
        }

        private bool HasDiceValue(int value, List<Die> dice, out Die d)
        {
            foreach (var di in dice)
            {
                if (di.Value == value)
                {
                    d = di;
                    return true;
                }
            }

            d = null;
            return false;
        }

        public override List<Die> GetScoredDice(List<Die> dice)
        {
            // Bit better. Still mediocre
            List<Die> scored = new();
            for (int i = 1; i <= 6; i++)
            {
                if (HasDiceValue(i, dice, out Die d))
                {
                    scored.Add(d);
                    for (int j = i + 1; j <= 6; j++)
                    {
                        if (HasDiceValue(j, dice, out Die d2))
                        {
                            scored.Add(d2);
                        }
                    }

                    if (scored.Count >= GetLength())
                    {
                        return scored;
                    }
                }
            }

            return scored;
        }
    }
}
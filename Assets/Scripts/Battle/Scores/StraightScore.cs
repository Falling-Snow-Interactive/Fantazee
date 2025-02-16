using System;
using System.Collections.Generic;
using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Battle.Scores
{
    public class StraightScore : Score
    {
        public override ScoreType Type { get; }
        private int length;

        public StraightScore(int length)
        {
            this.length = length;
            Type = length switch
                   {
                       3 => ScoreType.SmallStraight,
                       4 => ScoreType.LargeStraight,
                       _ => throw new ArgumentOutOfRangeException(nameof(length), length, null)
                   };
        }
        
        public override int Calculate(List<Die> dice)
        {
            // This works with 1,2,3,4,6 but not 1, 3,4,5,6
            Dictionary<int, int> dict = DiceToDict(dice);
            
            int startLimit = 7 - length;
            
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

                    if (count >= length)
                    {
                        return Type == ScoreType.SmallStraight ? 30 : 40;
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
                        if (HasDiceValue(j, dice, out var d2))
                        {
                            scored.Add(d2);
                        }
                    }

                    if (scored.Count >= length)
                    {
                        return scored;
                    }
                }
            }

            return scored;
        }
    }
}
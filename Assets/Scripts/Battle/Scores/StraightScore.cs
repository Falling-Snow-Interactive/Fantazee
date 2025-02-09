using System;
using System.Collections.Generic;

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
        
        public override int Calculate(List<Dices.Dice> dice)
        {
            // This works with 1,2,3,4,6 but not 1, 3,4,5,6
            Dictionary<int, int> dict = DiceToDict(dice);
            
            int startLimit = 6 - length;
            
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
                        return s;
                    }
                }
            }

            return 0;
        }
    }
}
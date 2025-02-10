using System;
using System.Collections.Generic;
using System.Linq;

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

        // This is probably a really bad way to do this...
        public override List<Dices.Dice> GetScoredDice(List<Dices.Dice> dice)
        {
            List<Dices.Dice> scored = new List<Dices.Dice>();

            List<int> straight = new();
            
            // This works with 1,2,3,4,6 but not 1, 3,4,5,6
            Dictionary<int, int> dict = DiceToDict(dice);
            
            int startLimit = 7 - length;
            
            for (int i = 1; i <= startLimit; i++)
            {
                if (dict.TryGetValue(i, out _))
                {
                    int count = 1;
                    for (int j = i + 1; j <= 6; j++)
                    {
                        if (dict.TryGetValue(j, out _))
                        {
                            count++;
                        }
                        else
                        {
                            if (count > straight.Count)
                            {
                                for (int x = i; x <= j; x++)
                                {
                                    straight.Add(x);
                                }
                            }
                            break;
                        }
                    }

                    if (count >= length)
                    {
                        var rem = new List<Dices.Dice>(dice);
                        foreach (var s in straight)
                        {
                            bool found = false;
                            foreach (Dices.Dice r in new List<Dices.Dice>(rem))
                            {
                                if (r.Value == s)
                                {
                                    rem.Remove(r);
                                    if (!found)
                                    {
                                        found = true;
                                        scored.Add(r);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return scored;
        }
    }
}
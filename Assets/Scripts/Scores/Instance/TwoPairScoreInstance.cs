using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class TwoPairScoreInstance : ScoreInstance
    {
        [SerializeReference]
        private TwoPairScoreData twoPairData;
        public TwoPairScoreData TwoPairData => twoPairData;
        
        public TwoPairScoreInstance(TwoPairScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.twoPairData = data;
        }

        public TwoPairScoreInstance(TwoPairScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.twoPairData = data;
        }

        public override int Calculate(List<Die> dice)
        {
            int score = 0;
            
            List<int> values = new();
            foreach (Die d in dice)
            {
                values.Add(d.Value);
            }
            
            values.Sort();
            values.Reverse();

            // search for 2 pairs. 
            int check = -1;
            int count = 0;
            int pairs = 0;

            foreach (int d in values)
            {
                if (check == -1)
                {
                    check = d;
                    count = 1;
                    continue;
                }

                if (check == d)
                {
                    count++;

                    if (count >= 2)
                    {
                        pairs++;
                        count = 0;
                        score += check * 2;
                        check = -1;
                    }
                }
                else
                {
                    check = d;
                    count = 1;
                }
            }

            return pairs >= 2 ? score : 0;
        }
    }
}
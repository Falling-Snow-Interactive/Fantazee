using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.SaveLoad;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public class RunScoreInstance : ScoreInstance
    {
        [Header("Run")]
        
        [SerializeReference]
        private RunScoreData runData;
        public RunScoreData RunData => runData;
        
        public RunScoreInstance(RunScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.runData = data;
        }

        public RunScoreInstance(RunScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.runData = data;
        }

        public RunScoreInstance(ScoreSave save) : base(save)
        {
            runData = save.Data as RunScoreData;
        }

        public override int Calculate(List<Die> dice)
        {
            List<int> values = new();
            foreach (Die d in dice)
            {
                if (!values.Contains(d.Value))
                {
                    values.Add(d.Value);
                }
            }
            
            values.Sort();
            values.Reverse();
            
            int prev = -1;
            int score = 0;
            int run = 0;
            // 6, 5, 3, 2, 1
            foreach (int value in values)
            {
                if (prev < 0)
                {
                    prev = value;
                    score = prev;
                    run = 1;
                    continue;
                }

                if (value == prev - 1)
                {
                    prev = value;
                    score += prev;
                    run++;
                }
                else
                {
                    prev = value;
                    score = 0;
                    run = 0;
                }
                
                if (run >= runData.Run)
                {
                    return score;
                }
            }

            return 0;
        }
    }
}
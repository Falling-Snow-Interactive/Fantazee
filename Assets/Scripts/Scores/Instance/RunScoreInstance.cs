using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
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

        public override int Calculate(List<Die> dice)
        {
            List<int> values = new();
            foreach (Die d in dice)
            {
                values.Add(d.Value);
            }
            
            values.Sort();
            values.Reverse();

            int run = 0;
            int score = 0;

            int curr = -1;
            int testScore = 0;
            int testRun = 0;
            // 6, 5, 3, 2, 1
            foreach (int value in values)
            {
                if (curr < 0)
                {
                    curr = value;
                    testScore = curr;
                    testRun = 1;
                    continue;
                }

                if (value == curr - 1)
                {
                    curr = value;
                    testScore += curr;
                    testRun++;
                }
                else
                {
                    if (testRun > run)
                    {
                        score = testScore;
                        run = testRun;
                    }
                    
                    curr = 0;
                    testScore = 0;
                    testRun = 0;
                }
            }
            
            if (testRun > run)
            {
                score = testScore;
            }

            return run >= runData.Run ? score : 0;
        }
    }
}
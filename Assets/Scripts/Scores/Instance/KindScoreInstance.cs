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
    public class KindScoreInstance : ScoreInstance
    {
        [Header("Kind")]
        
        [SerializeReference]
        private KindScoreData kindData;
        public KindScoreData KindData => kindData;
        
        public KindScoreInstance(KindScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.kindData = data;
        }

        public KindScoreInstance(KindScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.kindData = data;
        }

        public KindScoreInstance(ScoreSave save) : base(save)
        {
            kindData = save.Data as KindScoreData;
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

            int checking = -1;
            int count = 0;

            foreach (int value in values)
            {
                if (checking == -1)
                {
                    checking = value;
                    count++;
                    continue;
                }

                if (checking == value)
                {
                    count++;

                    if (count >= kindData.Kind)
                    {
                        return checking * count;
                    }
                }
                else
                {
                    count = 1;
                    checking = value;
                }
            }

            return 0;
        }
    }
}
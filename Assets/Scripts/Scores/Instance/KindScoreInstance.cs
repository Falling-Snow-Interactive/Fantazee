using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class KindScoreInstance : ScoreInstance
    {
        [Header("Kind")]
        
        [SerializeReference]
        private KindScoreData data;
        public new KindScoreData Data => data;
        
        public KindScoreInstance(KindScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.data = data;
        }

        public KindScoreInstance(KindScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.data = data;
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            bool isValid = false;
            int total = 0;

            foreach (KeyValuePair<int, int> kvp in dict)
            {
                total += kvp.Key * kvp.Value;
                if (kvp.Value >= data.Kind)
                {
                    isValid = true;
                }
            }

            return isValid ? total : 0;
        }
    }
}
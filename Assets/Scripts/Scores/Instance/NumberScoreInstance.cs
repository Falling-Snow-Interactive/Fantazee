using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class NumberScoreInstance : ScoreInstance
    {
        [Header("Number")]
        
        [SerializeReference]
        private NumberScoreData numberData;
        public NumberScoreData NumberData => numberData;
        
        public NumberScoreInstance(NumberScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.numberData = data;
        }

        public NumberScoreInstance(NumberScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.numberData = data;
        }

        public override int Calculate(List<Die> dice)
        {
            int sum = 0;
            foreach (Die d in dice)
            {
                if (d.Value == numberData.Number)
                {
                    sum += d.Value;
                }
            }

            return sum;
        }
    }
}
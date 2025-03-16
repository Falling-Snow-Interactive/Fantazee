using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    public class EvenOddScoreInstance : ScoreInstance
    {
        [SerializeReference]
        private EvenOddScoreData evenOddData;
        public EvenOddScoreData EvenOddData => evenOddData;
        
        public EvenOddScoreInstance(EvenOddScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.evenOddData = data;
        }

        public EvenOddScoreInstance(EvenOddScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.evenOddData = data;
        }

        public override int Calculate(List<Die> dice)
        {
            int check = evenOddData.Even ? 0 : 1;
            int sum = 0;
            foreach (Die d in dice)
            {
                if (d.Value % 2 == check)
                {
                    sum += d.Value;
                }
            }

            return sum;
        }
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public class ChanceScoreInstance : ScoreInstance
    {
        [Header("Chance")]
        
        [SerializeReference]
        private ChanceScoreData chanceData;
        
        public ChanceScoreInstance(ChanceScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.chanceData = data;
        }

        public ChanceScoreInstance(ChanceScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.chanceData = data;
        }

        public override int Calculate(List<Die> dice)
        {
            int score = 0;
            foreach (Die d in dice)
            {
                score += d.Value;
            }

            return score;
        }
    }
}
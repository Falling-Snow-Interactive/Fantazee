using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public class FantazeeScoreInstance : ScoreInstance
    {
        [Header("Fantazee")]
        [SerializeReference]
        private FantazeeScoreData data;
        
        public FantazeeScoreInstance(FantazeeScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.data = data;
        }

        public override int Calculate(List<Die> dice)
        {
            Dictionary<int, int> dict = DiceToDict(dice);
            bool isValid = false;
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                if (kvp.Value >= 5)
                {
                    isValid = true;
                }
            }

            return isValid ? 30 : 0;
        }
    }
}
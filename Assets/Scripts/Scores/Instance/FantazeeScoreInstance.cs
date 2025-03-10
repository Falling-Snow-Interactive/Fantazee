using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public class FantazeeScoreInstance : ScoreInstance
    {
        public override int MaxSpells => 3;

        [Header("Fantazee")]
        [SerializeReference]
        private FantazeeScoreData fantazeeData;
        
        public FantazeeScoreInstance(FantazeeScoreData data, List<SpellData> spells) : base(data, spells)
        {
            this.fantazeeData = data;
        }

        public FantazeeScoreInstance(FantazeeScoreData data, List<SpellInstance> spells) : base(data, spells)
        {
            this.fantazeeData = data;
        }

        public FantazeeScoreInstance(CharacterScoreData data) : base(data.Score, data.Spells)
        {
            this.fantazeeData = data.Score as FantazeeScoreData;
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
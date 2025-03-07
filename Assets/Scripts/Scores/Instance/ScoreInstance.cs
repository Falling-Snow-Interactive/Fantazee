using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Scores.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Scores.Instance
{
    [Serializable]
    public abstract class ScoreInstance
    {
        [Header("Score")]
        
        [SerializeReference]
        private ScoreData data;
        
        public ScoreInstance(ScoreData data, List<SpellInstance> spellTypes)
        {
            this.data = data;
        }

        public abstract int Calculate(List<Die> dice);
        
        protected Dictionary<int, int> DiceToDict(List<Die> dice)
        {
            Dictionary<int, int> diceByValue = new();
            foreach (Die d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] += 1;
                }
            }
            
            return diceByValue;
        }
    }
}

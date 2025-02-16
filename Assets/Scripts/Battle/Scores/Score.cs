using System;
using System.Collections.Generic;
using ProjectYahtzee.Items.Dice;
using UnityEngine;

namespace ProjectYahtzee.Battle.Scores
{
    [Serializable]
    public abstract class Score : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        public abstract ScoreType Type { get; }

        protected Score()
        {
        }

        public abstract int Calculate(List<Die> dice);
        
        public abstract List<Die> GetScoredDice(List<Die> dice);

        protected Dictionary<int, int> DiceToDict(List<Die> dice)
        {
            Dictionary<int, int> diceByValue = new Dictionary<int, int>();
            foreach (Die d in dice)
            {
                if (!diceByValue.TryAdd(d.Value, 1))
                {
                    diceByValue[d.Value] += 1;
                }
            }
            
            return diceByValue;
        }

        public void OnBeforeSerialize()
        {
            name = $"{Type}";
        }

        public void OnAfterDeserialize() { }
    }
}
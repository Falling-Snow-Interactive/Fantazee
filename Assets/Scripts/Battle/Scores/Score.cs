using System;
using System.Collections.Generic;
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

        public abstract int Calculate(List<Dices.Dice> dice);

        public abstract List<Dices.Dice> GetScoredDice(List<Dices.Dice> dice);

        protected Dictionary<int, int> DiceToDict(List<Dices.Dice> dice)
        {
            Dictionary<int, int> diceByValue = new Dictionary<int, int>();
            foreach (Dices.Dice d in dice)
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
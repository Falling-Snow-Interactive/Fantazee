using System;
using System.Collections.Generic;
using Fantazee.Dice;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public abstract class Score : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;

        [SerializeField]
        private ScoreType type;
        public ScoreType Type
        {
            get => type;
            set => type = value;
        }

        [SerializeField]
        private List<Die> dice = new();
        public List<Die> Dice => dice;

        public bool CanScore()
        {
            Debug.LogWarning("Score: CanScore not implemented. Returning true.");
            return dice.Count == 0;
        }

        public int AddDie(Die die)
        {
            dice.Add(die);
            Debug.LogWarning("Die added, now what?");
            return Calculate();
        }

        public void ClearDice()
        {
            dice.Clear();
            Debug.LogWarning("Dice cleared, now what?");
        }

        public abstract int Calculate();
        
        public abstract List<Die> GetScoredDice();

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

        public void OnBeforeSerialize()
        {
            name = $"{Type}";
        }

        public void OnAfterDeserialize() { }
    }
}
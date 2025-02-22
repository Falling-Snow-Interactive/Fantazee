using System;
using System.Collections.Generic;
using Fantazee.Dice;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public abstract class Score : ISerializationCallbackReceiver
    {
        public event Action Changed;
        public event Action DieAdded;
        public event Action DiceCleared;
        
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
            return dice.Count == 0;
        }

        public void AddDie(Die die)
        {
            dice.Add(die);
            string s = "";
            for (int i = 0; i < dice.Count; i++)
            {
                Die d = dice[i];
                s += $"{d.Value}";
                if (i != dice.Count - 1)
                {
                    s += " - ";
                }
            }

            Debug.Log($"Score: {Type} - Die: {die.Value}\n{s}");
            DieAdded?.Invoke();
            Changed?.Invoke();
        }

        public void ClearDice()
        {
            dice.Clear();
            Debug.LogWarning("Dice cleared, now what?");
            DiceCleared?.Invoke();
            Changed?.Invoke();
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
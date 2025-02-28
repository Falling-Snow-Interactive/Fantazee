using System;
using System.Collections.Generic;
using Fantazee.Battle.Settings;
using Fantazee.Dice;
using Fantazee.Scores.Information;
using Fantazee.Spells;
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
        protected ScoreType type;
        public ScoreType Type
        {
            get => type;
            set => type = value;
        }
        
        [SerializeField]
        private List<SpellType> spells;
        public List<SpellType> Spells
        {
            get => spells;
            set => spells = value;
        }

        private ScoreInformation information;
        public ScoreInformation Information
        {
            get
            {
                if (information == null)
                {
                    BattleSettings.Settings.ScoreInformation.TryGetInformation(type, out information);
                }
                
                return information;
            }
        } 

        protected Score(ScoreType type, List<SpellType> spells)
        {
            this.type = type;
            this.spells = new List<SpellType>(spells);
            while(spells.Count < 2)
            {
                spells.Add(SpellType.None);
            }
        }

        public abstract int Calculate(List<Die> dice);
        
        public abstract List<Die> GetScoredDice(List<Die> dice);

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
            string s = $"{Type}";
            foreach (SpellType spell in spells)
            {
                s += $"- {spell}";
            }

            name = s;
        }

        public void OnAfterDeserialize() { }
    }
}
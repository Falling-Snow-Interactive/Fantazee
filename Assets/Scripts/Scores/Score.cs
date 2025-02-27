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
        private SpellType spell;
        public SpellType Spell
        {
            get => spell;
            set => spell = value;
        }
        
        [SerializeField]
        private List<SpellType> spells = new List<SpellType>();
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

        protected Score(ScoreType type, SpellType spell)
        {
            this.type = type;
            this.spell = spell;
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
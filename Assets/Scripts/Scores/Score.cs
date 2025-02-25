using System;
using System.Collections.Generic;
using Fantazee.Dice;
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
            name = $"{Type} - {spell}";
        }

        public void OnAfterDeserialize() { }
    }
}
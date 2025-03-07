using System;
using System.Collections.Generic;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public class PlayerScoreData : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private ScoreType score;
        public ScoreType Score => score;
        
        [SerializeField]
        private List<SpellType> spells;
        public List<SpellType> Spells => spells;

        public override string ToString()
        {
            string s = $"{score}";
            foreach (SpellType spell in spells)
            {
                s += $" - {spell}";
            }

            return s;
        }
        
        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public class ScoreData : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private ScoreType score;
        public ScoreType Score => score;
        
        [SerializeField]
        private SpellType spell;
        public SpellType Spell => spell;
        
        [SerializeField]
        private List<SpellType> spells;
        public List<SpellType> Spells => spells;
        
        public void OnBeforeSerialize()
        {
            name = $"{score} - {spell}";
        }

        public void OnAfterDeserialize() { }
    }
}
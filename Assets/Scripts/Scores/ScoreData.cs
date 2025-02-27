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
        
        public void OnBeforeSerialize()
        {
            name = $"{score} - {spell}";
        }

        public void OnAfterDeserialize() { }
    }
}
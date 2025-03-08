using System;
using System.Collections.Generic;
using Fantazee.Scores.Data;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Characters
{
    [Serializable]
    public class CharacterScoreData : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private ScoreData score;
        public ScoreData Score => score;

        [SerializeField]
        private List<SpellData> spells;
        public List<SpellData> Spells => spells;

        public override string ToString()
        {
            string s = $"{score}: ";
            foreach (SpellData spell in spells)
            {
                s += $"{spell}";

                if (spell != spells[^1])
                {
                    s += " ";
                }
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
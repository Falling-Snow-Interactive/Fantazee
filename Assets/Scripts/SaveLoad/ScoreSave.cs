using System;
using System.Collections.Generic;
using Fantazee.Scores.Data;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class ScoreSave
    {
        [SerializeField]
        private ScoreData data;
        public ScoreData Data => data;
        
        [SerializeField]
        private List<SpellSave> spells;
        public List<SpellSave> Spells => spells;
        
        public ScoreSave(ScoreInstance instance)
        {
            data = instance.Data;

            spells = new List<SpellSave>();
            foreach (SpellInstance spell in instance.Spells)
            {
                SpellSave ss = new(spell);
                spells.Add(ss);
            }
        }
    }
}
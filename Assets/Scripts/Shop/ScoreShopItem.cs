using System;
using System.Collections.Generic;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [Serializable]
    public class ScoreShopItem
    {
        [SerializeField]
        private ScoreData data;
        public ScoreData Data => data;

        [SerializeField]
        private List<SpellData> spells = new();
        public List<SpellData> Spells => spells;

        public override string ToString()
        {
            string s = $"{data.name}";
            if (spells.Count > 0)
            {
                s += ": ";
                foreach (SpellData spell in spells)
                {
                    s += $"{spell.name}";
                    if (spell != spells[^1])
                    {
                        s += " - ";
                    }
                }
            }
            return s;
        }
    }
}
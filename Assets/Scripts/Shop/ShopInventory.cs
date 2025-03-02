using System;
using System.Collections.Generic;
using Fantazee.Scores;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<SpellType> spells; 
        public List<SpellType> Spells => spells;
        
        [SerializeField]
        private List<ScoreData> scores;
        public List<ScoreData> Scores => scores;

        public ShopInventory(List<SpellType> spells, List<ScoreData> scores)
        {
            this.spells = spells;
            this.scores = scores;
        }

        public override string ToString()
        {
            string s = "Spells:\n";
            for (int i = 0; i < spells.Count; i++)
            {
                SpellType spell = spells[i];
                s += $"\t{spell.ToString()}\n";
                if (i == spells.Count - 1)
                {
                    s += "\n";
                }
            }

            s += "Scores:\n";
            foreach (ScoreData score in scores)
            {
                s += $"\t{score}\n";

                if (score != scores[^1])
                {
                    s += "\n";
                }
            }

            return s;
        }
    }
}
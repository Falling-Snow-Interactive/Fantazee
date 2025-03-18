using System;
using System.Collections.Generic;
using Fantazee.Relics.Data;
using Fantazee.Relics.Instance;
using Fantazee.Scores.Data;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<SpellData> spells; 
        public List<SpellData> Spells => spells;
        
        [SerializeField]
        private List<ScoreShopItem> scores;
        public List<ScoreShopItem> Scores => scores;
        
        [SerializeField]
        private List<RelicData> relics;
        public List<RelicData> Relics => relics;

        public ShopInventory(List<SpellData> spells, 
                             List<ScoreShopItem> scores, 
                             List<RelicData> relics)
        {
            this.spells = spells;
            this.scores = scores;
            this.relics = relics;
        }

        public void Remove(List<RelicInstance> relics)
        {
            foreach (RelicData r in new List<RelicData>(this.relics))
            foreach (RelicInstance ri in relics)
            {
                if (r.Type == ri.Data.Type)
                {
                    this.relics.Remove(r);
                }
            }
        }

        public void Purge(int spellCount, int scoreCount, int relicCount)
        {
            while (spells.Count > spellCount)
            {
                spells.RemoveAt(0);
            }

            while (scores.Count > scoreCount)
            {
                scores.RemoveAt(0);
            }

            while (relics.Count > relicCount)
            {
                relics.RemoveAt(0);
            }
        }

        public override string ToString()
        {
            string s = "Spells:\n";
            foreach (SpellData spell in spells)
            {
                s += $"\t{spell}\n";
                if (spell == spells[^1])
                {
                    s += "\n";
                }
            }

            s += "Scores:\n";
            foreach (ScoreShopItem score in scores)
            {
                s += $"\t{score}\n";
                if (score != scores[^1])
                {
                    s += "\n";
                }
            }
            
            s += "Relics:\n";
            foreach (RelicData relic in relics)
            {
                s += $"\t{relic}\n";
                if (relic != relics[^1])
                {
                    s += "\n";
                }
            }

            return s;
        }
    }
}
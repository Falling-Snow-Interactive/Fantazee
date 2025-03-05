using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Relics.Information;
using Fantazee.Relics.Instance;
using Fantazee.Scores;
using Fantazee.Shop.Items;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<SpellShopItem> spells; 
        public List<SpellShopItem> Spells => spells;
        
        [SerializeField]
        private List<ScoreShopItem> scores;
        public List<ScoreShopItem> Scores => scores;
        
        [SerializeField]
        private List<RelicShopItem> relics;
        public List<RelicShopItem> Relics => relics;

        public ShopInventory(List<SpellShopItem> spells, 
                             List<ScoreShopItem> scores, 
                             List<RelicShopItem> relics)
        {
            this.spells = spells;
            this.scores = scores;
            this.relics = relics;
        }

        public void Remove(List<RelicInstance> relics)
        {
            foreach (RelicShopItem r in new List<RelicShopItem>(this.relics))
            foreach (RelicInstance ri in relics)
            {
                if (r.Item.Type == ri.Data.Type)
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
            foreach (var spell in spells)
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
            foreach (RelicShopItem relic in relics)
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
using System;
using System.Collections.Generic;
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

        public ShopInventory(List<SpellShopItem> spells, List<ScoreShopItem> scores)
        {
            this.spells = spells;
            this.scores = scores;
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

            return s;
        }
    }
}
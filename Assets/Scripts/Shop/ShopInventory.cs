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
        private List<SpellType> spells = new(); 
        public List<SpellType> Spells => spells;
        
        [SerializeField]
        private List<Score> scores = new();
        public List<Score> Scores => scores;
    }
}
using System;
using System.Collections.Generic;
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
    }
}
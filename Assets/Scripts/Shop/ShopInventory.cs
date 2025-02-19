using System;
using System.Collections.Generic;
using ProjectYahtzee.Boons;
using UnityEngine;

namespace ProjectYahtzee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<BoonType> boons = new(); 
        public List<BoonType> Boons => boons;
    }
}
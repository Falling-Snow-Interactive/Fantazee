using System;
using System.Collections.Generic;
using Fantahzee.Boons;
using UnityEngine;

namespace Fantahzee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<BoonType> boons = new(); 
        public List<BoonType> Boons => boons;
    }
}
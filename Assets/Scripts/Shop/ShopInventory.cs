using System;
using System.Collections.Generic;
using Fantazhee.Boons;
using UnityEngine;

namespace Fantazhee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<BoonType> boons = new(); 
        public List<BoonType> Boons => boons;
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Boons;
using UnityEngine;

namespace Fantazee.Shop
{
    [Serializable]
    public class ShopInventory
    {
        [SerializeField]
        private List<BoonType> boons = new(); 
        public List<BoonType> Boons => boons;
    }
}
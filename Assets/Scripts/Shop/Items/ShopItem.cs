using System;
using Fantazee.Currencies;
using UnityEngine;

namespace Fantazee.Shop.Items
{
    [Serializable]
    public abstract class ShopItem<T>
    {
        [SerializeField]
        private Currency cost;
        public Currency Cost => cost;
        
        public abstract T Item { get; }

        public ShopItem(Currency cost)
        {
            this.cost = cost;
        }
        
        public override string ToString()
        {
            return $"{Item} | {Cost}";
        }
    }
}
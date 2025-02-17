using System;
using System.Collections.Generic;
using ProjectYahtzee.Blacksmith.Cart.Entries;

namespace ProjectYahtzee.Blacksmith.Cart
{
    public class BlacksmithCart
    {
        public event Action Updated;
        
        public List<BlacksmithCartEntry> Entries { get; }
        
        public BlacksmithCart()
        {
            Entries = new List<BlacksmithCartEntry>();
        }

        public void AddToCart(BlacksmithCartEntry entry)
        {
            Entries.Add(entry);
            Updated?.Invoke();
        }

        public void RemoveFromCart(BlacksmithCartEntry entry)
        {
            Entries.Remove(entry);
            Updated?.Invoke();
        }
    }
}
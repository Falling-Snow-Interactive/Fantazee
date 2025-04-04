using System;
using Fantazee.Dice;
using Fantazee.Items.Dice;

namespace Fantazee.Blacksmith.Cart.Entries
{
    public static class BlacksmithCartEntryFactory
    {
        public static BlacksmithCartEntry Create(Die die, int side, BlacksmithCartEntryType type, int cost, int change)
        {
            return type switch
                   {
                       BlacksmithCartEntryType.Value => new CartValueEntry(die, side, cost, change),
                       BlacksmithCartEntryType.Weight => new CartWeightEntry(die, side, cost, change),
                       _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                   };
        }
    }
}
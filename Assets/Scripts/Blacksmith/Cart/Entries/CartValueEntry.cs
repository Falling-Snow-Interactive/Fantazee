using Fantahzee.Dice;
using Fantahzee.Items.Dice;

namespace Fantahzee.Blacksmith.Cart.Entries
{
    public class CartValueEntry : BlacksmithCartEntry
    {
        public override BlacksmithCartEntryType Type => BlacksmithCartEntryType.Value;

        public CartValueEntry(Die die, int side, int cost, int change) : base(die, side, cost, change)
        {
        }
    }
}
using Fantazhee.Items.Dice;
using Fantazhee.Dice;

namespace Fantazhee.Blacksmith.Cart.Entries
{
    public class CartValueEntry : BlacksmithCartEntry
    {
        public override BlacksmithCartEntryType Type => BlacksmithCartEntryType.Value;

        public CartValueEntry(Die die, int side, int cost, int change) : base(die, side, cost, change)
        {
        }
    }
}
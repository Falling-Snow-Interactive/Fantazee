using ProjectYahtzee.Dice;
using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Blacksmith.Cart.Entries
{
    public class CartValueEntry : BlacksmithCartEntry
    {
        public override BlacksmithCartEntryType Type => BlacksmithCartEntryType.Value;

        public CartValueEntry(Die die, int side, int cost, int change) : base(die, side, cost, change)
        {
        }
    }
}
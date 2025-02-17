using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Blacksmith.Cart.Entries
{
    public class CartWeightEntry : BlacksmithCartEntry
    {
        public override BlacksmithCartEntryType Type => BlacksmithCartEntryType.Weight;
        
        public CartWeightEntry(Die die, int side, int cost, int change) : base(die, side, cost, change)
        {
        }
    }
}
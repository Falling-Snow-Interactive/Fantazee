using ProjectYahtzee.Dice;
using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Blacksmith.Cart.Entries
{
    public abstract class BlacksmithCartEntry
    {
        public abstract BlacksmithCartEntryType Type { get; }
        public int Cost { get; }
        public int Change { get; }
        
        public Die Die { get; }
        public int Side { get; }

        public BlacksmithCartEntry(Die die, int side, int cost, int change)
        {
            Cost = cost;
            Change = change;
            Die = die;
            Side = side;
        }
    }
}
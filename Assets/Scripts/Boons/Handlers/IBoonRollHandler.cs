using System.Collections.Generic;
using ProjectYahtzee.Items.Dice;

namespace ProjectYahtzee.Boons.Handlers
{
    public interface IBoonRollHandler
    {
        public void OnDiceRoll(Die die);
    }
}
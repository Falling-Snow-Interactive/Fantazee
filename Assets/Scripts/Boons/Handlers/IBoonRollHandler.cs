using System.Collections.Generic;

namespace ProjectYahtzee.Boons.Handlers
{
    public interface IBoonRollHandler
    {
        public void OnDiceRoll(Battle.Dices.Dice dice);
    }
}
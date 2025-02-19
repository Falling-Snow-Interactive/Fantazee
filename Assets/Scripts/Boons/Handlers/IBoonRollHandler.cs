using System.Collections.Generic;
using Fantahzee.Dice;
using Fantahzee.Items.Dice;

namespace Fantahzee.Boons.Handlers
{
    public interface IBoonRollHandler
    {
        public void OnDiceRoll(Die die);
    }
}
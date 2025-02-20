using System.Collections.Generic;
using Fantazhee.Items.Dice;
using Fantazhee.Dice;

namespace Fantazhee.Boons.Handlers
{
    public interface IBoonRollHandler
    {
        public void OnDiceRoll(Die die);
    }
}
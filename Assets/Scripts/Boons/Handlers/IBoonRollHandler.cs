using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Items.Dice;

namespace Fantazee.Boons.Handlers
{
    public interface IBoonRollHandler
    {
        public void OnDiceRoll(Die die);
    }
}
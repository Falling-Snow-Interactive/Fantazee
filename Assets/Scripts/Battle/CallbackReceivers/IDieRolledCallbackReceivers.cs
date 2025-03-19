using System;
using Fantazee.Dice;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IDieRolledCallbackReceivers
    {
        public void OnDieRolled(Die die, Action onComplete);
    }
}
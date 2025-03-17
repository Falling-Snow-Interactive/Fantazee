using System;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface ITurnStartCallbackReceiver
    {
        public void OnTurnStart(Action onComplete);
    }
}
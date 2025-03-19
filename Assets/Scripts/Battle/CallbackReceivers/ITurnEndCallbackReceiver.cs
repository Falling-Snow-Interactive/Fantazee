using System;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface ITurnEndCallbackReceiver
    {
        public void OnTurnEnd(Action onComplete);
    }
}
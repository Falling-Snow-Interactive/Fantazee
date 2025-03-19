using System;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IRollStartedCallbackReceiver
    {
        public void OnRollStarted(Action onComplete);
    }
}
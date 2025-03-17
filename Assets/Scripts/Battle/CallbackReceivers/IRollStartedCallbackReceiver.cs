using System;
using System.Collections;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IRollStartedCallbackReceiver
    {
        public void OnRollStarted(Action onComplete);
    }
}
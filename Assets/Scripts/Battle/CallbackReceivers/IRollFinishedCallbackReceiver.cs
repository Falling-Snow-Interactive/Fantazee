using System;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IRollFinishedCallbackReceiver
    {
        public void OnRollFinished(Action onComplete);
    }
}
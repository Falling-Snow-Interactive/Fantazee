using System;
using Fantazee.Scores;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IDealingDamageCallbackReceivers
    {
        public void OnDealDamage(ScoreResults result, Action<ScoreResults> onComplete);
    }
}
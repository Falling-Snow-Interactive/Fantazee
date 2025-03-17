using System;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IScoreCallbackReceiver
    {
        public void OnScore(ref Scores.ScoreResults scoreResults, Action onComplete);
    }
}
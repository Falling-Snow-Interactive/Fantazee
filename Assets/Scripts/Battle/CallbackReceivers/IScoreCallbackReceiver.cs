using System;
using Fantazee.Scores;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IScoreCallbackReceiver
    {
        public void OnScore(ScoreResults scoreResults, Action<ScoreResults> onComplete);
    }
}
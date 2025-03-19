using System;
using Fantazee.Scores;

namespace Fantazee.Battle.CallbackReceivers
{
    public interface IScoreModifier
    {
        public ScoreResults ModifyScore(ScoreResults results);

        public void ModifyScoreWithCallback(ScoreResults scoreResults, Action<ScoreResults> onComplete); 
    }
}
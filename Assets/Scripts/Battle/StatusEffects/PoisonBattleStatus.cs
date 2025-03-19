using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.Scores;
using Fantazee.StatusEffects.Data;
using UnityEngine;

namespace Fantazee.Battle.StatusEffects
{
    [Serializable]
    public class PoisonBattleStatus : BattleStatusEffect, IScoreCallbackReceiver
    {
        public PoisonBattleStatus(PoisonStatusData data, int turns, BattleCharacter character) : base(data, turns, character)
        {
        }
        
        public override void Activate()
        {
            base.Activate();
            Character.RegisterScoreReceiver(this);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Character.UnregisterScoreReceiver(this);
        }

        public void OnScore(ScoreResults scoreResults, Action<ScoreResults> onComplete)
        {
            // Should probably play some kind of effect
            scoreResults.Value = Mathf.Max(0, scoreResults.Value - TurnsRemaining);
            onComplete?.Invoke(scoreResults);
        }
    }
}
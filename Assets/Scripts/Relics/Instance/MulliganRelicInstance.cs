using System;
using Fantazee.Battle;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class MulliganRelicInstance : RelicInstance, IScoreCallbackReceiver
    {
        private bool startRoll;
        private bool firstRoll;
        private bool hasScored;
        
        public MulliganRelicInstance(RelicData data, CharacterInstance character) : base(data, character)
        {
            firstRoll = false;
            hasScored = false;
            startRoll = false;
        }

        public override void Enable()
        {
            BattleController.BattleStarted += OnBattleStart;
            BattleController.BattleEnded += OnBattleEnd;
        }

        public override void Disable()
        {
            BattleController.BattleEnded -= OnBattleEnd;
            BattleController.BattleStarted -= OnBattleStart;
        }

        private void OnBattleStart()
        {
            BattleController.Instance.Player.TurnStarted += OnTurnStart;
            BattleController.Instance.Player.RollStarted += OnRollStarted;
            BattleController.Instance.Player.RegisterScoreReceiver(this);
        }

        private void OnBattleEnd()
        {
            BattleController.Instance.Player.TurnStarted -= OnTurnStart;
            BattleController.Instance.Player.RollStarted -= OnRollStarted;
            BattleController.Instance.Player.UnregisterScoreReceiver(this);
        }
        
        private void OnTurnStart()
        {
            startRoll = true;
            firstRoll = true;
            hasScored = false;
        }
        
        private void OnRollStarted()
        {
            if (hasScored)
            {
                return;
            }
            
            if (startRoll)
            {
                startRoll = false;
                return;
            }

            if (!firstRoll)
            {
                return;
            }
            
            firstRoll = false;
            if (BattleController.Instance.Player.LockedDice.Count == 0)
            {
                Debug.Log($"Mulligan: Activated");
                BattleController.Instance.Player.RollsRemaining++;
                Activate();
            }
        }

        public void OnScore(ref ScoreResults scoreResults, Action onComplete)
        {
            hasScored = true;
            onComplete?.Invoke();
        }
    }
}
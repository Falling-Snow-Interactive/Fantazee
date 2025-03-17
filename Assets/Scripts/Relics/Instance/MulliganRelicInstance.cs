using System;
using Fantazee.Battle;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class MulliganRelicInstance : RelicInstance, 
                                         ITurnStartCallbackReceiver, 
                                         IRollStartedCallbackReceiver, 
                                         IScoreCallbackReceiver
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
            BattleController.Instance.Player.RegisterTurnStartReceiver(this);
            BattleController.Instance.Player.RegisterRollStartedReceiver(this);
            BattleController.Instance.Player.RegisterScoreReceiver(this);
        }

        private void OnBattleEnd()
        {
            BattleController.Instance.Player.UnregisterTurnStartReceiver(this);
            BattleController.Instance.Player.UnregisterRollStartedReceiver(this);
            BattleController.Instance.Player.UnregisterScoreReceiver(this);
        }
        
        public void OnTurnStart(Action onComplete)
        {
            startRoll = true;
            firstRoll = true;
            hasScored = false;
        }

        public void OnRollStarted(Action onComplete)
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
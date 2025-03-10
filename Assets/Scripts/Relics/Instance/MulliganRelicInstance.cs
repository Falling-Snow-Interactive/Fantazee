using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class MulliganRelicInstance : RelicInstance
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
            BattleController.PlayerTurnStart += OnPlayerStart;
            BattleController.RollStarted += OnRollStarted;
            BattleController.Scored += OnScored;
        }

        public override void Disable()
        {
            BattleController.PlayerTurnStart -= OnPlayerStart;
            BattleController.RollStarted -= OnRollStarted;
            BattleController.Scored -= OnScored;
        }

        private void OnPlayerStart()
        {
            startRoll = true;
            firstRoll = true;
            hasScored = false;
        }

        private void OnScored(BattleScore _)
        {
            hasScored = true;
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
            if (BattleController.Instance.LockedDice.Count == 0)
            {
                Debug.Log($"Mulligan: Activated");
                BattleController.Instance.RemainingRolls++;
                Activate();
            }
        }
    }
}
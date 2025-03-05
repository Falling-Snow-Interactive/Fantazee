using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Battle.Ui;
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
            BattleController.PlayerTurnStart += OnPlayerStart;
            BattleController.RollStarted += OnRollStarted;
            BattleController.Scored += OnScored;

            firstRoll = false;
            hasScored = false;
            startRoll = false;
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
                Debug.Log("Mulligan: start roll.");
                startRoll = false;
            }
            else
            {
                if (firstRoll)
                {
                    firstRoll = false;
                    Debug.Log($"Mulligan: Locked {BattleController.Instance.LockedDice.Count}.");
                    if (BattleController.Instance.LockedDice.Count == 0)
                    {
                        BattleController.Instance.RemainingRolls++;
                        Activate();
                    }
                }
            }
        }
    }
}
using ProjectYahtzee.Battle;
using ProjectYahtzee.Boons.Handlers;
using UnityEngine;

namespace ProjectYahtzee.Boons.GiveTake
{
    public class GiveTakeBoon : Boon, IBoonDamageHandler
    {
        public override BoonType Type => BoonType.GiveTake;

        private float value;
        private bool scoredRoll = true;
        
        public Boon Boon => this;

        public GiveTakeBoon()
        {
            BattleController.Scored += OnScored;
            BattleController.RollStarted += OnRollStarted;
        }
        
        private void OnScored(int obj)
        {
            scoredRoll = true;
            value++;
            entryUi.UpdateUi();
            entryUi.Squish();
        }

        private void OnRollStarted()
        {
            if (scoredRoll)
            {
                scoredRoll = false;
                return;
            }

            value = Mathf.Clamp(value - 1, 0, Mathf.Infinity);
            entryUi.UpdateUi();
            entryUi.Squish();
        }
        
        public override string GetBonusText()
        {
            return $"+{value}";
        }
        
        public void ReceiveDamage(ref Damage damage)
        {
            damage.Value += Mathf.RoundToInt(value);
        }
    }
}
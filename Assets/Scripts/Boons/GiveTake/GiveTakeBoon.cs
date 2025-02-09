using ProjectYahtzee.Battle;
using UnityEngine;

namespace ProjectYahtzee.Boons.GiveTake
{
    public class GiveTakeBoon : Boon
    {
        public override BoonType Type => BoonType.GiveTake;

        private float value;
        private bool scoredRoll = true;

        public GiveTakeBoon()
        {
            BattleController.Scored += OnScored;
            BattleController.Rolled += OnRolled;
        }

        public override float GetBonus() => value;
        
        private void OnScored(int obj)
        {
            scoredRoll = true;
            value++;
            entryUi.UpdateUi();
            entryUi.Squish();
        }

        private void OnRolled()
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
    }
}
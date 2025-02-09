using ProjectYahtzee.Battle;
using UnityEngine;

namespace ProjectYahtzee.Boons.GiveTake
{
    public class GiveTakeBoon : Boon
    {
        public override BoonType Type => BoonType.GiveTake;

        private float mod;
        private bool scoredRoll = true;

        public GiveTakeBoon()
        {
            BattleController.Scored += OnScored;
            BattleController.Rolled += OnRolled;
        }

        public override float GetModifier() => mod;
        
        private void OnScored(int obj)
        {
            scoredRoll = true;
            mod++;
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

            mod = Mathf.Clamp(mod - 1, 0, Mathf.Infinity);
            entryUi.UpdateUi();
            entryUi.Squish();
        }
    }
}
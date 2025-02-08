using System;
using ProjectYahtzee.Battle;
using UnityEngine;

namespace ProjectYahtzee.Boons.TwoMod
{
    [Serializable]
    public class TwoModBoon : Boon
    {
        public override BoonType Type => BoonType.TwoMod;

        public int mod;

        public TwoModBoon() : base()
        {
            mod = 0;

            BattleController.DiceScored += OnDiceScored;
        }

        private void OnDiceScored(int dice)
        {
            if (dice == 2)
            {
                mod++;
            }
        }

        public override float GetModifier()
        {
            return mod;
        }
    }
}
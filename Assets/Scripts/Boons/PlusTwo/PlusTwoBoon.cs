using System;
using ProjectYahtzee.Battle;

namespace ProjectYahtzee.Boons.PlusTwo
{
    [Serializable]
    public class PlusTwoBoon : Boon
    {
        public override BoonType Type => BoonType.PlusTwo;

        public int value;

        public PlusTwoBoon() : base()
        {
            value = 0;

            BattleController.DiceScored += OnDiceScored;
        }

        private void OnDiceScored(int dice)
        {
            if (dice == 2)
            {
                value += 2;
                entryUi.Squish();
                entryUi.UpdateUi();
            }
        }

        public override float GetValue()
        {
            return value;
        }
    }
}
using System;
using ProjectYahtzee.Battle;
using ProjectYahtzee.Boons.Handlers;

namespace ProjectYahtzee.Boons.PlusTwo
{
    [Serializable]
    public class PlusTwoBoon : Boon, IBoonDamageHandler
    {
        public override BoonType Type => BoonType.PlusTwo;
        public Boon Boon => this;

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
        
        public override string GetBonusText()
        {
            return $"+{value}";
        }

        public void ReceiveDamage(ref Damage damage)
        {
            damage.Value += value;
        }
    }
}
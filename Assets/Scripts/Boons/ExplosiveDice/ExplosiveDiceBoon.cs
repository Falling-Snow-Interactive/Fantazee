using ProjectYahtzee.Battle;

namespace ProjectYahtzee.Boons.ExplosiveDice
{
    public class ExplosiveDiceBoon : Boon
    {
        public override BoonType Type => BoonType.ExplosiveDice;

        public ExplosiveDiceBoon() : base()
        {
            BattleController.DiceRolled += OnDiceRolled;
        }

        private void OnDiceRolled(Battle.Dices.Dice dice)
        {
            if (dice.Value == 6)
            {
                foreach (var enemy in BattleController.Instance.Enemies)
                {
                    enemy.Damage(6);
                }
            }
        }
        
        public override string GetBonusText()
        {
            return "6";
        }
    }
}
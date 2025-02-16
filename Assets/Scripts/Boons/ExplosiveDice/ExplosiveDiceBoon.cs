using ProjectYahtzee.Battle;
using ProjectYahtzee.Battle.Characters.Enemies;
using ProjectYahtzee.Boons.Handlers;

namespace ProjectYahtzee.Boons.ExplosiveDice
{
    public class ExplosiveDiceBoon : Boon, IBoonRollHandler
    {
        public override BoonType Type => BoonType.ExplosiveDice;

        private readonly int roll;

        public ExplosiveDiceBoon(int roll) : base()
        {
            this.roll = roll;
        }
        
        public override string GetBonusText()
        {
            return roll.ToString();
        }

        public void OnDiceRoll(Battle.Dices.Dice dice)
        {
            if (dice.Value == roll)
            {
                foreach (GameplayEnemy enemy in BattleController.Instance.Enemies)
                {
                    enemy.Damage(roll);
                }
            }
        }
    }
}
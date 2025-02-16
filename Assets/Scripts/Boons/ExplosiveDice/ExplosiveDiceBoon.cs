using System;
using ProjectYahtzee.Battle;
using ProjectYahtzee.Battle.Characters.Enemies;
using ProjectYahtzee.Boons.Handlers;
using UnityEngine;

namespace ProjectYahtzee.Boons.ExplosiveDice
{
    [Serializable]
    public class ExplosiveDiceBoon : Boon, IBoonRollHandler
    {
        public override BoonType Type => BoonType.ExplosiveDice;

        [SerializeField]
        private int roll;

        public ExplosiveDiceBoon(int roll) : base()
        {
            this.roll = roll;
        }
        
        public override string GetBonusText()
        {
            return roll.ToString();
        }

        public void OnDiceRoll(Battle.Dice.Die die)
        {
            if (die.Value == roll)
            {
                foreach (GameplayEnemy enemy in BattleController.Instance.Enemies)
                {
                    enemy.Damage(roll);
                }
            }
        }
    }
}
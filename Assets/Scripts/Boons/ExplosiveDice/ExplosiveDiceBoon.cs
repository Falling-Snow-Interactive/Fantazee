using System;
using Fantazhee.Items.Dice;
using Fantazhee.Battle;
using Fantazhee.Battle.Characters.Enemies;
using Fantazhee.Boons.Handlers;
using Fantazhee.Dice;
using UnityEngine;

namespace Fantazhee.Boons.ExplosiveDice
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

        public void OnDiceRoll(Die die)
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
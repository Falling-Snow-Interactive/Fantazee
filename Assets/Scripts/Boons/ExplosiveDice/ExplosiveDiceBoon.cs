using System;
using Fantahzee.Battle;
using Fantahzee.Battle.Characters.Enemies;
using Fantahzee.Boons.Handlers;
using Fantahzee.Dice;
using Fantahzee.Items.Dice;
using UnityEngine;

namespace Fantahzee.Boons.ExplosiveDice
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
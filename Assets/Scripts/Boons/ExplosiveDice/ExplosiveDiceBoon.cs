using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Boons.Handlers;
using Fantazee.Dice;
using Fantazee.Items.Dice;
using UnityEngine;

namespace Fantazee.Boons.ExplosiveDice
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
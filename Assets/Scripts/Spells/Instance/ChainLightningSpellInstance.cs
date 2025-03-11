using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    public class ChainLightningSpellInstance : SpellInstance
    {
        private ChainLightningSpellData lightningData;
        
        public ChainLightningSpellInstance(ChainLightningSpellData lightningData) : base(lightningData)
        {
            this.lightningData = lightningData;
        }

        protected override void Apply(Damage damage)
        {
            if (BattleController.Instance.Enemies.Count > 0)
            {
                BattleEnemy e0 = BattleController.Instance.Enemies[0];
                BattleEnemy e1 = BattleController.Instance.Enemies.Count > 1 
                                     ? BattleController.Instance.Enemies[1] 
                                     : null;

                if (e1)
                {
                    int d0 = Mathf.Max(Mathf.RoundToInt(damage.Value * lightningData.FirstEnemyMod), 1);
                    int d1 = Mathf.Max(Mathf.RoundToInt(damage.Value * lightningData.SecondEnemyMod), 1);
                    
                    e0.Damage(d0);
                    e1.Damage(d1);
                }
                else
                {
                    e0.Damage(damage.Value);
                }
            }
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy) 
                       ? enemy.transform.position + lightningData.ProjectileHitOffset
                       : Vector3.zero;
        }
    }
}
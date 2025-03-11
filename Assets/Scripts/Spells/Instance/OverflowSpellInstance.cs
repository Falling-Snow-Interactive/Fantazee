using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Spells.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class OverflowSpellInstance : SpellInstance
    {
        private OverflowSpellData overflowData;
        
        public OverflowSpellInstance(OverflowSpellData data) : base(data)
        {
            overflowData = data;
        }

        protected override void Apply(Damage damage)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                int total = damage.Value;
                int d = enemy.Damage(damage.Value);
                int rem = total - d;
                
                if (overflowData.HitVfx)
                {
                    Object.Instantiate(overflowData.HitVfx,
                                       enemy.transform.position + overflowData.ProjectileHitOffset,
                                       enemy.transform.rotation);
                }

                if (rem > 0 && BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
                {
                    front.Damage(rem);
                    
                    if (overflowData.HitVfx)
                    {
                        Object.Instantiate(overflowData.HitVfx,
                                           front.transform.position + overflowData.ProjectileHitOffset,
                                           front.transform.rotation);
                    }
                }
            }
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy) 
                       ? enemy.transform.position + overflowData.ProjectileHitOffset
                       : Vector3.zero;
        }
    }
}
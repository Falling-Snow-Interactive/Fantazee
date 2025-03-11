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

                while (rem > 0 && BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
                {
                    int o = front.Damage(rem);
                    rem -= o;
                    
                    if (overflowData.HitAnim.Vfx)
                    {
                        Object.Instantiate(overflowData.HitAnim.Vfx,
                                           front.transform.position + overflowData.HitAnim.Offset,
                                           front.transform.rotation);
                    }
                }
            }
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy) 
                       ? enemy.transform.position + overflowData.HitAnim.Offset
                       : Vector3.zero;
        }
    }
}
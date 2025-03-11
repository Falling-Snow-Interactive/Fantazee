using System;
using System.Collections.Generic;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Spells.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class PierceSpellInstance : SpellInstance
    {
        private PierceSpellData data;

        public PierceSpellInstance(PierceSpellData data) : base(data)
        {
            this.data = data;
        }
        
        protected override void Apply(Damage damage, Action onComplete)
        {
            if (BattleController.Instance.Enemies.Count > 0)
            {
                List<BattleEnemy> rand = new(BattleController.Instance.Enemies);
                
                BattleEnemy e0 = BattleController.Instance.Enemies[0];
                rand.Remove(e0);
                
                if (rand.Count > 0)
                {
                    BattleEnemy e1 = rand[Random.Range(0, rand.Count)];
                    int d0 = Mathf.Max(Mathf.RoundToInt(damage.Value * data.FirstEnemyMod), 1);
                    int d1 = Mathf.Max(Mathf.RoundToInt(damage.Value * data.SecondEnemyMod), 1);
                    
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
            return BattleController.Instance.Enemies.Count switch
                   {
                       1 => BattleController.Instance.Enemies[0].transform.position + data.HitAnim.Offset,
                       > 1 => (BattleController.Instance.Enemies[0].transform.position +
                               BattleController.Instance.Enemies[1].transform.position) 
                              / 2f +
                              data.HitAnim.Offset,
                       _ => Vector3.zero
                   };
        }
    }
}
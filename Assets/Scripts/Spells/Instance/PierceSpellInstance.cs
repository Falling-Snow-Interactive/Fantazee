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
                if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy front))
                {
                    int index = BattleController.Instance.Enemies.IndexOf(front);
                    index++;
                    if (BattleController.Instance.Enemies.Count > index)
                    {
                        BattleEnemy second = BattleController.Instance.Enemies[index];
                        front.Damage(Mathf.Max(Mathf.RoundToInt(damage.Value * data.FirstEnemyMod), 1));
                        second.Damage(Mathf.Max(Mathf.RoundToInt(damage.Value * data.SecondEnemyMod), 1));
                    }
                    else
                    {
                        front.Damage(damage.Value);
                    }
                }
            }
            
            onComplete?.Invoke();
        }

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.Enemies.Count switch
                   {
                       1 => BattleController.Instance.Enemies[0].transform.position + data.HitAnim.Offset,
                       > 1 => BattleController.Instance.Enemies[1].transform.position + data.HitAnim.Offset,
                       _ => Vector3.zero
                   };
        }
    }
}
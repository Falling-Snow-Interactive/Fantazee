using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class FireballSpellInstance : SpellInstance
    {
        private FireballSpellData data;
        
        public FireballSpellInstance(FireballSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(Damage damage)
        {
            int d = Mathf.Max(Mathf.RoundToInt(damage.Value * data.DamageMod));
            foreach (BattleEnemy enemy in BattleController.Instance.Enemies)
            {
                enemy.Damage(d);
            }
        }

        protected override Vector3 GetHitPos()
        {
            Vector3 hitPos = Vector3.zero;
            foreach (BattleEnemy e in BattleController.Instance.Enemies)
            {
                hitPos += e.transform.position + data.HitAnim.Offset;
            }
            return hitPos / BattleController.Instance.Enemies.Count;
        }
    }
}
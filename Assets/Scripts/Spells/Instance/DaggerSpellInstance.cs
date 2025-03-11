using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class DaggerSpellInstance : SpellInstance
    {
        private DaggerSpellData data;
        
        public DaggerSpellInstance(DaggerSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(Damage damage)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                enemy.Damage(damage.Value);
            }
        }

        protected override Vector3 GetHitPos()
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                return enemy.transform.position + data.HitAnim.Offset;
            }
            
            return Vector3.zero;
        }
    }
}
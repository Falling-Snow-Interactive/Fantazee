using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using Fantazee.StatusEffects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Spells.Instance
{
    public class BiteSpellInstance : SpellInstance
    {
        private BiteSpellData data;
        
        public BiteSpellInstance(BiteSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                enemy.Damage(scoreResults.Value);

                if (Random.value < data.Roll)
                {
                    enemy.AddStatusEffect(StatusEffectType.status_01_bleed, data.Turns);
                }
            }
            
            onComplete?.Invoke();
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
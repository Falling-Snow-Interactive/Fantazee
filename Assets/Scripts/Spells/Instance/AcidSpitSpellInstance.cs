using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class AcidSpitSpellInstance : SpellInstance
    {
        private AcidSpitSpellData data;
        
        public AcidSpitSpellInstance(AcidSpitSpellData data) : base(data)
        {
            this.data = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                enemy.Damage(scoreResults.Value);

                if (Random.value < data.Status.Chance)
                {
                    enemy.AddStatusEffect(data.Status.Data, data.Status.Turns);
                }
            }
            
            onComplete?.Invoke();
        }

        protected override void OnCast()
        {
            BattleController.Instance.Player.Visuals.Attack();
            base.OnCast();
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
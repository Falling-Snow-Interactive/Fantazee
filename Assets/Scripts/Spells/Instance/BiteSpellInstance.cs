using System;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;

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

                if (data.Status.Roll())
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
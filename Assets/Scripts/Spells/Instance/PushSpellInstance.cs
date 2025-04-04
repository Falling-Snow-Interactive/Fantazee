using System;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Scores;
using Fantazee.Spells.Data;

namespace Fantazee.Spells.Instance
{
    public class PushSpellInstance : SpellInstance
    {
        private readonly PushSpellData pushData;
        
        public PushSpellInstance(PushSpellData data) : base(data)
        {
            pushData = data;
        }

        protected override void Apply(ScoreResults scoreResults, Action onComplete)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                int d = Mathf.RoundToInt(scoreResults.Value * pushData.PushDamageMod);
                enemy.Damage(d);
                if (enemy.Health.IsAlive)
                {
                    PushToBack(enemy, onComplete);
                }
                else
                {
                    onComplete?.Invoke();
                }
            }
            else
            {
                onComplete?.Invoke();
            }
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
                return enemy.transform.position + pushData.HitAnim.Offset;
            }
            
            return Vector3.zero;
        }

        private void PushToBack(BattleEnemy push, Action onComplete = null)
        {
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;

            Vector3 pos = Vector3.zero;

            enemies.Remove(push);
            enemies.Add(push);

            float offset = 0.05f;
            pos.y = offset;

            Sequence sequence = DOTween.Sequence();
            
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                BattleEnemy enemy = enemies[i];
                
                Tween tween = enemy.transform.DOLocalMove(pos, pushData.MoveTime)
                                   .SetEase(pushData.MoveEase);
                sequence.Insert(pushData.MoveDelay * (enemies.Count - i), tween);
                
                pos.x -= enemy.Data.Size.x;
                pos.y = offset;
                offset *= -1;
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
    }
}
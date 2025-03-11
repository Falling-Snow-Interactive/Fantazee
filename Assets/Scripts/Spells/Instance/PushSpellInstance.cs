using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Spells.Instance
{
    public class PushSpellInstance : SpellInstance
    {
        private readonly PushSpellData pushData;
        
        public PushSpellInstance(PushSpellData data) : base(data)
        {
            pushData = data;
        }

        protected override void Apply(Damage damage)
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                int d = Mathf.RoundToInt(damage.Value * pushData.PushDamageMod);
                enemy.Damage(d);
                if (enemy.Health.IsAlive)
                {
                    PushToBack();
                }
            }
        }

        protected override Vector3 GetHitPos()
        {
            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                return enemy.transform.position + pushData.ProjectileHitOffset;
            }
            
            return Vector3.zero;
        }

        private void PushToBack(Action onComplete = null)
        {
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;

            Vector3 root = enemies[0].transform.position;
            Vector3 pos = root;
            
            BattleEnemy push = enemies[^1];
            enemies.Remove(push);
            enemies.Insert(0, push);

            Sequence sequence = DOTween.Sequence();
            for (int i = 0; i < enemies.Count; i++)
            {
                BattleEnemy enemy = enemies[i];
                Tween tween = enemy.transform.DOMove(pos, pushData.MoveTime)
                                   .SetEase(pushData.MoveEase);
                sequence.Insert(pushData.MoveDelay * i, tween);
                
                float y = i % 2 == 0 
                              ? root.y + 0.05f 
                              : root.y - 0.05f;
                pos.x -= enemy.Size;
                pos.y = y;
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
    }
}
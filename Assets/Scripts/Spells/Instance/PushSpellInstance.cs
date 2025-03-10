using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Spells.Instance
{
    public class PushSpellInstance : SpellInstance
    {
        private PushSpellData pushData;

        private EventInstance pushSfx;
        private EventInstance hitSfx;
        
        public PushSpellInstance(PushSpellData data) : base(data)
        {
            pushData = data;
            
            if (!pushData.PushSfx.IsNull)
            {
                pushSfx = RuntimeManager.CreateInstance(pushData.PushSfx);
            }
            
            if (!pushData.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(pushData.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            bool ready = false;

            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                player.Visuals.Attack();
                GameObject tweenVfx = Object.Instantiate(pushData.PushVfx, player.transform);
                tweenVfx.transform.localPosition = pushData.PushSpawnOffset;
                pushSfx.start();
                if (pushData.PushEase == Ease.INTERNAL_Custom)
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + pushData.PushHitOffset,
                                              pushData.PushTime)
                            .SetEase(pushData.PushCurve)
                            .SetDelay(pushData.PushDelay)
                            .OnComplete(() =>
                                        {
                                            pushSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                else
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + pushData.PushHitOffset,
                                              pushData.PushTime)
                            .SetEase(pushData.PushEase)
                            .SetDelay(pushData.PushDelay)
                            .OnComplete(() =>
                                        {
                                            pushSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }

                yield return new WaitUntil(() => ready);

                int d = Mathf.RoundToInt(damage.Value * pushData.PushDamageMod);
                enemy.Damage(d);
                
                if (pushData.HitVfx)
                {
                    Object.Instantiate(pushData.HitVfx,
                                       enemy.transform.position + pushData.PushHitOffset,
                                       enemy.transform.rotation);
                }

                if (hitSfx.isValid())
                {
                    hitSfx.start();
                }
                
                if (enemy.Health.IsAlive)
                {
                    bool pushFinished = false;
                    PushToBack(() => pushFinished = true);
                    yield return new WaitUntil(() => pushFinished);
                }

                yield return new WaitForSeconds(1f);
                onComplete?.Invoke();
            }
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
                sequence.Insert(pushData.PushDelay * i, tween);
                
                float y = i % 2 == 0 ? root.y + 0.05f : root.y - 0.05f;
                pos.x -= enemy.Size;
                pos.y = y;
            }

            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }
    }
}
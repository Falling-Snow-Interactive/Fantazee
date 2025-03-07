using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMOD.Studio;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class FireballSpellInstance : SpellInstance
    {
        private FireballSpellData data;
        
        private EventInstance fireballSfx;
        private EventInstance hitSfx;
        
        public FireballSpellInstance(FireballSpellData data) : base(data)
        {
            this.data = data;
        }
        
        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            bool ready = false;

            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                player.Visuals.Attack();
                GameObject tweenVfx = Object.Instantiate(data.FireballVfx, player.transform);
                tweenVfx.transform.localPosition = data.FireballSpawnOffset;
                fireballSfx.start();
                if (data.FireballEase == Ease.INTERNAL_Custom)
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + data.FireballHitOffset,
                                              data.FireballTime)
                            .SetEase(data.FireballCurve)
                            .SetDelay(data.FireballDelay)
                            .OnComplete(() =>
                                        {
                                            fireballSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                else
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + data.FireballHitOffset,
                                              data.FireballTime)
                            .SetEase(data.FireballEase)
                            .SetDelay(data.FireballDelay)
                            .OnComplete(() =>
                                        {
                                            fireballSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }

                yield return new WaitUntil(() => ready);
                enemy.Damage(damage.Value);
                if (data.HitVfx)
                {
                    Object.Instantiate(data.HitVfx,
                                       enemy.transform.position + data.FireballHitOffset,
                                       enemy.transform.rotation);
                }

                if (hitSfx.isValid())
                {
                    hitSfx.start();
                }

                yield return new WaitForSeconds(1f);
                onComplete?.Invoke();
            }
        }
    }
}
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
using Object = UnityEngine.Object;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class OverflowSpellInstance : SpellInstance
    {
        private OverflowSpellData overflowData;

        private EventInstance overflowSfx;
        private EventInstance hitSfx;
        
        public OverflowSpellInstance(OverflowSpellData data) : base(data)
        {
            overflowData = data;
            
            if (!data.OverflowSfx.IsNull)
            {
                overflowSfx = RuntimeManager.CreateInstance(overflowData.OverflowSfx);
            }
            
            if (!data.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(overflowData.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            bool ready = false;

            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                player.Visuals.Attack();
                GameObject tweenVfx = Object.Instantiate(overflowData.OverflowVfx, player.transform);
                tweenVfx.transform.localPosition = overflowData.OverflowSpawnOffset;
                overflowSfx.start();
                if (overflowData.OverflowEase == Ease.INTERNAL_Custom)
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + overflowData.OverflowHitOffset,
                                              overflowData.OverflowTime)
                            .SetEase(overflowData.OverflowCurve)
                            .SetDelay(overflowData.OverflowDelay)
                            .OnComplete(() =>
                                        {
                                            overflowSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                else
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + overflowData.OverflowHitOffset,
                                              overflowData.OverflowTime)
                            .SetEase(overflowData.OverflowEase)
                            .SetDelay(overflowData.OverflowDelay)
                            .OnComplete(() =>
                                        {
                                            overflowSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }

                yield return new WaitUntil(() => ready);
                int total = damage.Value;
                // int delt = enemy.Damage(damage.Value);
                if (overflowData.HitVfx)
                {
                    Object.Instantiate(overflowData.HitVfx,
                                       enemy.transform.position + overflowData.OverflowHitOffset,
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
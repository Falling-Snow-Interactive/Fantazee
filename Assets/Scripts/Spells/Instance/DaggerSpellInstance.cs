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
    public class DaggerSpellInstance : SpellInstance
    {
        private DaggerSpellData data;

        private EventInstance daggerSfx;
        private EventInstance hitSfx;
        
        public DaggerSpellInstance(DaggerSpellData data) : base(data)
        {
            this.data = data;
            
            if (!data.DaggerSfx.IsNull)
            {
                daggerSfx = RuntimeManager.CreateInstance(data.DaggerSfx);
            }
            
            if (!data.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(data.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            bool ready = false;

            if (BattleController.Instance.TryGetFrontEnemy(out BattleEnemy enemy))
            {
                player.Visuals.Attack();
                GameObject tweenVfx = Object.Instantiate(data.DaggerVfx, player.transform);
                tweenVfx.transform.localPosition = data.DaggerSpawnOffset;
                daggerSfx.start();
                if (data.DaggerEase == Ease.INTERNAL_Custom)
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + data.DaggerHitOffset,
                                              data.DaggerTime)
                            .SetEase(data.DaggerCurve)
                            .SetDelay(data.DaggerDelay)
                            .OnComplete(() =>
                                        {
                                            daggerSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                else
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + data.DaggerHitOffset,
                                              data.DaggerTime)
                            .SetEase(data.DaggerEase)
                            .SetDelay(data.DaggerDelay)
                            .OnComplete(() =>
                                        {
                                            daggerSfx.stop(STOP_MODE.IMMEDIATE);
                                            Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }

                yield return new WaitUntil(() => ready);
                enemy.Damage(damage.Value);
                if (data.HitVfx)
                {
                    Object.Instantiate(data.HitVfx,
                                       enemy.transform.position + data.DaggerHitOffset,
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
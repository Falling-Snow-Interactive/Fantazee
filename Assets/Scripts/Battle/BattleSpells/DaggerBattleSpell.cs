using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Characters.Enemies;
using Fantazee.Battle.Characters.Player;
using Fantazee.Spells.Data;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public class DaggerBattleSpell : BattleSpell
    {
        private DaggerData dagger;
        
        private EventInstance daggerLoopSfx;
        private EventInstance daggerHitSfx;
        
        public DaggerBattleSpell(DaggerData data) : base(data)
        {
            dagger = data;
            if (!dagger.TweenSfx.IsNull)
            {
                daggerLoopSfx = RuntimeManager.CreateInstance(data.TweenSfx);
            }

            if (!dagger.HitSfx.IsNull)
            {
                daggerHitSfx = RuntimeManager.CreateInstance(data.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;
            BattleEnemy enemy = null;
            bool ready = false;
            
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Health.IsAlive)
                {
                    enemy = enemies[i];
                    break;
                }
            }

            if (enemy)
            {
                player.Visuals.Attack();
                GameObject tweenVfx = UnityEngine.Object.Instantiate(dagger.TweenVfx, player.transform);
                tweenVfx.transform.localPosition = dagger.TweenVfxSpawnOffset;
                daggerLoopSfx.start();
                tweenVfx.transform.DOMove(enemy.transform.position + dagger.TweenVfxHitOffset, dagger.TweenTime)
                        .SetEase(dagger.TweenEase)
                        .SetDelay(dagger.TweenDelay)
                        .OnComplete(() =>
                                    {
                                        daggerLoopSfx.stop(STOP_MODE.IMMEDIATE);
                                        UnityEngine.Object.Destroy(tweenVfx.gameObject);
                                        ready = true;
                                    });
                yield return new WaitUntil(() => ready);
                enemy.Damage(damage.Value);
                if (dagger.HitVfx)
                {
                    UnityEngine.Object.Instantiate(dagger.HitVfx,
                                                   enemy.transform.position + dagger.TweenVfxHitOffset,
                                                   enemy.transform.rotation);
                    daggerHitSfx.start();

                }

                if (daggerHitSfx.isValid())
                {
                    daggerHitSfx.start();
                }
            
                yield return new WaitForSeconds(1f);
                onComplete?.Invoke();
            }
        }
    }
}
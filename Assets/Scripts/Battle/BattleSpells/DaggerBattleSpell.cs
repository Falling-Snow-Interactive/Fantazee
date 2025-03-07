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
        private DaggerSpellData daggerSpell;
        
        private EventInstance daggerLoopSfx;
        private EventInstance daggerHitSfx;
        
        public DaggerBattleSpell(DaggerSpellData spellData) : base(spellData)
        {
            daggerSpell = spellData;
            if (!daggerSpell.TweenSfx.IsNull)
            {
                daggerLoopSfx = RuntimeManager.CreateInstance(spellData.TweenSfx);
            }

            if (!daggerSpell.HitSfx.IsNull)
            {
                daggerHitSfx = RuntimeManager.CreateInstance(spellData.HitSfx);
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
                GameObject tweenVfx = UnityEngine.Object.Instantiate(daggerSpell.TweenVfx, player.transform);
                tweenVfx.transform.localPosition = daggerSpell.TweenVfxSpawnOffset;
                daggerLoopSfx.start();
                if (daggerSpell.TweenEase == Ease.INTERNAL_Custom)
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + daggerSpell.TweenVfxHitOffset, daggerSpell.TweenTime)
                            .SetEase(daggerSpell.TweenCurve)
                            .SetDelay(daggerSpell.TweenDelay)
                            .OnComplete(() =>
                                        {
                                            daggerLoopSfx.stop(STOP_MODE.IMMEDIATE);
                                            UnityEngine.Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                else
                {
                    tweenVfx.transform.DOMove(enemy.transform.position + daggerSpell.TweenVfxHitOffset, daggerSpell.TweenTime)
                            .SetEase(daggerSpell.TweenEase)
                            .SetDelay(daggerSpell.TweenDelay)
                            .OnComplete(() =>
                                        {
                                            daggerLoopSfx.stop(STOP_MODE.IMMEDIATE);
                                            UnityEngine.Object.Destroy(tweenVfx.gameObject);
                                            ready = true;
                                        });
                }
                yield return new WaitUntil(() => ready);
                enemy.Damage(damage.Value);
                if (daggerSpell.HitVfx)
                {
                    UnityEngine.Object.Instantiate(daggerSpell.HitVfx,
                                                   enemy.transform.position + daggerSpell.TweenVfxHitOffset,
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
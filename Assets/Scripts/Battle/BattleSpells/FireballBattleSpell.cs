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
    public class FireballBattleSpell : BattleSpell
    {
        private readonly FireballSpellData fireballData;
        
        private EventInstance loopSfx;
        private EventInstance hitSfx;
        
        public FireballBattleSpell(FireballSpellData fireballData) : base(fireballData)
        {
            this.fireballData = fireballData;
            
            if (!fireballData.TweenSfx.IsNull)
            {
                loopSfx = RuntimeManager.CreateInstance(fireballData.TweenSfx);
            }

            if (!fireballData.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(fireballData.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;
            
            float count = enemies.Count;
            
            // TODO - should probably have a vfx here

            if (enemies.Count > 0)
            {
                player.Visuals.Attack();
                GameObject tweenVfx = UnityEngine.Object.Instantiate(fireballData.TweenVfx, player.transform);
                tweenVfx.transform.localPosition = fireballData.TweenVfxSpawnOffset;
                loopSfx.start();
                bool ready = false;
                tweenVfx.transform.DOMove(enemies[0].transform.position + fireballData.TweenVfxHitOffset, 
                                          fireballData.TweenTime)
                        .SetEase(fireballData.TweenEase)
                        .SetDelay(fireballData.TweenDelay)
                        .OnComplete(() =>
                                    {
                                        loopSfx.stop(STOP_MODE.IMMEDIATE);
                                        UnityEngine.Object.Destroy(tweenVfx.gameObject);
                                        ready = true;
                                    });

                yield return new WaitUntil(() => ready);

                int d = Mathf.RoundToInt(damage.Value * enemies.Count>1 ? fireballData.DamageMod : 1);
                foreach (BattleEnemy enemy in enemies)
                {
                    if (enemy.Health.IsAlive)
                    {
                        enemy.Damage(Mathf.RoundToInt(d));
                    }
                }

                Vector3 mid = enemies[0].transform.position;
                if (enemies.Count > 1)
                {
                    mid = (enemies[0].transform.position + enemies[^1].transform.position) / 2f;
                }

                if (fireballData.HitVfx)
                {
                    UnityEngine.Object.Instantiate(fireballData.HitVfx,
                                                   mid + fireballData.TweenVfxHitOffset,
                                                   Quaternion.identity);
                    hitSfx.start();

                }

                yield return new WaitForSeconds(1f);
            }

            onComplete?.Invoke();
        }
    }
}
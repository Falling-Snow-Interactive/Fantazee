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
using Random = UnityEngine.Random;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Spells.Instance
{
    public class ChainLightningSpellInstance : SpellInstance
    {
        private ChainLightningSpellData lightningData;
        
        private EventInstance daggerSfx;
        private EventInstance hitSfx;
        
        public ChainLightningSpellInstance(ChainLightningSpellData lightningData) : base(lightningData)
        {
            this.lightningData = lightningData;
            
            if (!lightningData.PierceSfx.IsNull)
            {
                daggerSfx = RuntimeManager.CreateInstance(lightningData.PierceSfx);
            }
            
            if (!lightningData.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(lightningData.HitSfx);
            }
        }

        protected override IEnumerator CastSequence(Damage damage, Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            List<BattleEnemy> enemies = BattleController.Instance.Enemies;

            player.Visuals.Attack();

            yield return new WaitForSeconds(0.2f);
            
            BattleEnemy e0 = enemies[^1];
            if (e0)
            {
                BattleEnemy e1 = null;
                if (enemies.Count > 1)
                {
                    var en = new List<BattleEnemy>(enemies);
                    en.Remove(e0);
                    e1 = en[Random.Range(0, en.Count)];
                }

                Vector3 startPos = player.transform.position + lightningData.PierceVfxSpawnOffset;
                Vector3 endPos = e0.transform.position + lightningData.PierceVfxHitOffset;

                bool ready = false;
                GameObject arrow = Object.Instantiate(lightningData.PierceVfx, startPos, Quaternion.identity);
                arrow.transform.DOMove(endPos, lightningData.PierceTime)
                     .SetDelay(lightningData.PierceDelay)
                     .SetEase(lightningData.PierceEase)
                     .OnComplete(() =>
                                 {
                                     Object.Destroy(arrow.gameObject);
                                     
                                     ready = true;
                                 });

                yield return new WaitUntil(() => ready);
                
                if (hitSfx.isValid())
                {
                    hitSfx.start();
                }
                e0.Damage(Mathf.RoundToInt(damage.Value * lightningData.FirstEnemyMod));
                if (lightningData.HitVfx)
                {
                    Object.Instantiate(lightningData.HitVfx,
                                       e0.transform.position + lightningData.PierceVfxHitOffset,
                                       e0.transform.rotation);
                }

                if (e1)
                {
                    e1.Damage(Mathf.RoundToInt(damage.Value * lightningData.SecondEnemyMod));
                    if (lightningData.HitVfx)
                    {
                        Object.Instantiate(lightningData.HitVfx,
                                           e1.transform.position + lightningData.PierceVfxHitOffset,
                                           e1.transform.rotation);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}
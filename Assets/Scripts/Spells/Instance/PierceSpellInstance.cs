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

namespace Fantazee.Spells.Instance
{
    [Serializable]
    public class PierceSpellInstance : SpellInstance
    {
        private PierceSpellData data;
        
        private EventInstance daggerSfx;
        private EventInstance hitSfx;

        public PierceSpellInstance(PierceSpellData data) : base(data)
        {
            this.data = data;
            
            if (!data.PierceSfx.IsNull)
            {
                daggerSfx = RuntimeManager.CreateInstance(data.PierceSfx);
            }
            
            if (!data.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(data.HitSfx);
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
                    e1 = enemies[^2];
                }

                Vector3 startPos = player.transform.position + data.PierceVfxSpawnOffset;
                Vector3 endPos = e0.transform.position + data.PierceVfxSpawnOffset;
                if (e1 != null)
                {
                    endPos = e1.transform.position + data.PierceVfxSpawnOffset;
                }

                bool ready = false;
                var arrow = Object.Instantiate(data.PierceVfx, startPos, Quaternion.identity);
                arrow.transform.DOMove(endPos, data.PierceTime)
                     .SetDelay(data.PierceDelay)
                     .SetEase(data.PierceEase)
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
                e0.Damage(Mathf.RoundToInt(damage.Value * data.FirstEnemyMod));
                if (data.HitVfx)
                {
                    Object.Instantiate(data.HitVfx,
                                       e0.transform.position + data.PierceVfxHitOffset,
                                       e0.transform.rotation);
                }

                if (e1)
                {
                    e1.Damage(Mathf.RoundToInt(damage.Value * data.SecondEnemyMod));
                    if (data.HitVfx)
                    {
                        Object.Instantiate(data.HitVfx,
                                           e1.transform.position + data.PierceVfxHitOffset,
                                           e1.transform.rotation);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}
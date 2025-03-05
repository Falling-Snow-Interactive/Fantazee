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
using Object = UnityEngine.Object;

namespace Fantazee.Battle.BattleSpells
{
    [Serializable]
    public class PierceBattleSpell : BattleSpell
    {
        private PierceData pierceData;
        
        private EventInstance pierceLoopSfx;
        private EventInstance pierceHitSfx;
        
        public PierceBattleSpell(PierceData data) : base(data)
        {
            pierceData = data;
            if (!pierceData.TweenSfx.IsNull)
            {
                pierceLoopSfx = RuntimeManager.CreateInstance(data.TweenSfx);
            }

            if (!pierceData.HitSfx.IsNull)
            {
                pierceHitSfx = RuntimeManager.CreateInstance(data.HitSfx);
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

                Vector3 startPos = player.transform.position + pierceData.TweenVfxSpawnOffset;
                Vector3 endPos = e0.transform.position + pierceData.TweenVfxSpawnOffset;
                if (e1 != null)
                {
                    endPos = e1.transform.position + pierceData.TweenVfxSpawnOffset;
                }

                bool ready = false;
                var arrow = Object.Instantiate(pierceData.TweenVfx, startPos, Quaternion.identity);
                arrow.transform.DOMove(endPos, pierceData.TweenTime)
                     .SetDelay(pierceData.TweenDelay)
                     .SetEase(pierceData.TweenEase)
                     .OnComplete(() =>
                                 {
                                     Object.Destroy(arrow.gameObject);
                                     
                                     ready = true;
                                 });

                yield return new WaitUntil(() => ready);
                
                if (pierceHitSfx.isValid())
                {
                    pierceHitSfx.start();
                }
                e0.Damage(Mathf.RoundToInt(damage.Value * pierceData.FirstEnemyMod));
                if (pierceData.HitVfx)
                {
                    Object.Instantiate(pierceData.HitVfx,
                                                   e0.transform.position + pierceData.TweenVfxHitOffset,
                                                   e0.transform.rotation);
                }

                if (e1)
                {
                    e1.Damage(Mathf.RoundToInt(damage.Value * pierceData.SecondEnemyMod));
                    if (pierceData.HitVfx)
                    {
                        Object.Instantiate(pierceData.HitVfx,
                                           e1.transform.position + pierceData.TweenVfxHitOffset,
                                           e1.transform.rotation);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}
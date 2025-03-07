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
        private PierceSpellData pierceSpellData;
        
        private EventInstance pierceLoopSfx;
        private EventInstance pierceHitSfx;
        
        public PierceBattleSpell(PierceSpellData spellData) : base(spellData)
        {
            pierceSpellData = spellData;
            if (!pierceSpellData.TweenSfx.IsNull)
            {
                pierceLoopSfx = RuntimeManager.CreateInstance(spellData.TweenSfx);
            }

            if (!pierceSpellData.HitSfx.IsNull)
            {
                pierceHitSfx = RuntimeManager.CreateInstance(spellData.HitSfx);
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

                Vector3 startPos = player.transform.position + pierceSpellData.TweenVfxSpawnOffset;
                Vector3 endPos = e0.transform.position + pierceSpellData.TweenVfxSpawnOffset;
                if (e1 != null)
                {
                    endPos = e1.transform.position + pierceSpellData.TweenVfxSpawnOffset;
                }

                bool ready = false;
                var arrow = Object.Instantiate(pierceSpellData.TweenVfx, startPos, Quaternion.identity);
                arrow.transform.DOMove(endPos, pierceSpellData.TweenTime)
                     .SetDelay(pierceSpellData.TweenDelay)
                     .SetEase(pierceSpellData.TweenEase)
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
                e0.Damage(Mathf.RoundToInt(damage.Value * pierceSpellData.FirstEnemyMod));
                if (pierceSpellData.HitVfx)
                {
                    Object.Instantiate(pierceSpellData.HitVfx,
                                                   e0.transform.position + pierceSpellData.TweenVfxHitOffset,
                                                   e0.transform.rotation);
                }

                if (e1)
                {
                    e1.Damage(Mathf.RoundToInt(damage.Value * pierceSpellData.SecondEnemyMod));
                    if (pierceSpellData.HitVfx)
                    {
                        Object.Instantiate(pierceSpellData.HitVfx,
                                           e1.transform.position + pierceSpellData.TweenVfxHitOffset,
                                           e1.transform.rotation);
                    }
                }
            }

            yield return new WaitForSeconds(1f);
            
            onComplete?.Invoke();
        }
    }
}
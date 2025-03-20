using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Intentions;
using Fantazee.StatusEffects;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Battle.Characters.Enemies.Actions
{
    public abstract class AttackAction : EnemyAction
    {
        public override Intention Intention { get; }

        private AttackActionData data;

        protected AttackAction(AttackActionData data, BattleEnemy source, BattleCharacter target) : base(data, source, target)
        {
            this.data = data;

            Intention = new Intention(IntentionType.intention_00_attack, data.Damage.Random());
        }

        public override void Perform(Action onComplete)
        {
            Source.StartCoroutine(AttackSequence(onComplete));
        }

        private void DamagePlayer()
        {
            BattleController.Instance.Player.Damage(Intention.Amount); // TODO do this properly.
            if (data.StatusEffect.Data.Type != StatusEffectType.status_none 
                && UnityEngine.Random.value <= data.StatusEffect.Chance)
            {
                Target.AddStatusEffect(data.StatusEffect.Data.Type, data.StatusEffect.Turns);
            }
        }

        private IEnumerator AttackSequence(Action onComplete = null)
        {
            bool ready;
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                ready = false;
                BattleController.Instance.StartCoroutine(CastSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            if (data.ProjectileAnim.HasProjectile)
            {
                ready = false;
                BattleController.Instance.StartCoroutine(ProjectileSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }
            
            DamagePlayer();
            if (data.HitAnim.HasHit)
            {
                HitSequence();
                DamagePlayer();
            }

            onComplete?.Invoke();
        }

        private IEnumerator CastSequence(Action onComplete = null)
        {
            if (data.CastAnim.CastVfx)
            {
                Object.Instantiate(data.CastAnim.CastVfx, BattleController.Instance.Player.transform);
            }

            if (!data.CastAnim.CastSfx.IsNull)
            {
                RuntimeManager.PlayOneShot(data.CastAnim.CastSfx);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            onComplete?.Invoke();
        }

        private IEnumerator ProjectileSequence(Action onComplete = null)
        {
            bool ready = false;

            Vector3 hitPos = GetHitPos();
            
            Source.Visuals.Attack();
            GameObject projectileVfx = Object.Instantiate(data.ProjectileAnim.Vfx, Source.transform);
            projectileVfx.transform.localPosition = data.ProjectileAnim.SpawnOffset;

            if (!data.ProjectileAnim.Sfx.IsNull)
            {
                RuntimeManager.PlayOneShot(data.ProjectileAnim.Sfx);
            }
            
            projectileVfx.transform.DOMove(hitPos, data.ProjectileAnim.Time)
                         .SetEase(data.ProjectileAnim.Ease)
                         .SetDelay(data.ProjectileAnim.Delay)
                         .OnComplete(() =>
                                     {
                                         Object.Destroy(projectileVfx.gameObject);
                                         ready = true;
                                     });
            
            yield return new WaitUntil(() => ready);

            onComplete?.Invoke();
        }

        private void HitSequence()
        {
            Vector3 hitPos = GetHitPos();
            if (data.HitAnim.Vfx)
            {
                Object.Instantiate(data.HitAnim.Vfx, hitPos, Quaternion.identity);
            }

            if (!data.HitAnim.Sfx.IsNull)
            {
                RuntimeManager.PlayOneShot(data.HitAnim.Sfx);
            }
        }

        protected virtual Vector3 GetHitPos()
        {
            return Vector3.zero;
        }
    }
}
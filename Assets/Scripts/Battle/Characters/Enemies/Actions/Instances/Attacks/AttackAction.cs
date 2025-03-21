using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData.Attacks;
using Fantazee.Battle.Characters.Intentions;
using Fantazee.StatusEffects;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fantazee.Battle.Characters.Enemies.Actions.Instances.Attacks
{
    public class AttackAction : EnemyAction
    {
        public override ActionType Type => ActionType.action_00_attack;
        public override Intention Intention { get; }

        private readonly AttackActionData data;

        public AttackAction(AttackActionData data, BattleEnemy source) : base(data, source)
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
            if (data.StatusEffect.Data 
                && data.StatusEffect.Data.Type != StatusEffectType.status_none 
                && UnityEngine.Random.value <= data.StatusEffect.Chance)
            {
                BattleController.Instance.Player.AddStatusEffect(data.StatusEffect.Data.Type, data.StatusEffect.Turns);
            }
        }

        private IEnumerator AttackSequence(Action onComplete = null)
        {
            bool ready;
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                ready = false;
                Source.StartCoroutine(CastSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }
            
            if (data.ProjectileAnim.HasProjectile)
            {
                ready = false;
                Source.StartCoroutine(ProjectileSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }
            
            DamagePlayer();
            if (data.HitAnim.HasHit)
            {
                HitSequence();
            }

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

        protected override Vector3 GetHitPos()
        {
            return BattleController.Instance.Player.transform.position + data.HitAnim.Offset;
        }
    }
}
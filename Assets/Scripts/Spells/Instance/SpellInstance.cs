using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle;
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
    public abstract class SpellInstance
    {
        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        private EventInstance castSfx;
        private EventInstance projectileSfx;
        private EventInstance hitSfx;

        protected SpellInstance(SpellData data)
        {
            this.data = data;

            if (!data.CastAnim.CastSfx.IsNull)
            {
                castSfx = RuntimeManager.CreateInstance(data.CastAnim.CastSfx);
            }
            
            if (!data.ProjectileAnim.Sfx.IsNull)
            {
                projectileSfx = RuntimeManager.CreateInstance(data.ProjectileAnim.Sfx);
            }
            
            if (!data.HitAnim.Sfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(data.HitAnim.Sfx);
            }
        }
        
        public void Cast(Damage damage, Action onComplete = null)
        {
            if (castSfx.isValid())
            {
                castSfx.start();
            }

            if (data.CastAnim.CastVfx)
            {
                Object.Instantiate(data.CastAnim.CastVfx, BattleController.Instance.Player.transform);
            }
            
            BattleController.Instance.StartCoroutine(SpellSequence(damage, onComplete));
        }

        protected abstract void Apply(Damage damage);

        private IEnumerator SpellSequence(Damage damage, Action onComplete = null)
        {
            yield return new WaitForSeconds(0.25f);
            
            if (data.CastAnim.HasCast)
            {
                bool ready = false;
                BattleController.Instance.StartCoroutine(CastSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            if (data.ProjectileAnim.HasProjectile)
            {
                bool ready = false;
                BattleController.Instance.StartCoroutine(ProjectileSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            Apply(damage);
            
            if (data.HitAnim.HasHit)
            {
                bool ready = false;
                BattleController.Instance.StartCoroutine(HitSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            onComplete?.Invoke();
        }

        private IEnumerator CastSequence(Action onComplete = null)
        {
            yield return new WaitForSeconds(0.5f);
            
            onComplete?.Invoke();
        }

        private IEnumerator ProjectileSequence(Action onComplete = null)
        {
            BattlePlayer player = BattleController.Instance.Player;
            bool ready = false;

            Vector3 hitPos = GetHitPos();
            
            player.Visuals.Attack();
            GameObject projectileVfx = Object.Instantiate(data.ProjectileAnim.Vfx, player.transform);
            projectileVfx.transform.localPosition = data.ProjectileAnim.SpawnOffset;
            projectileSfx.start();
            
            if (data.ProjectileAnim.Ease is Ease.INTERNAL_Custom or Ease.INTERNAL_Zero)
            {
                projectileVfx.transform.DOMove(hitPos, data.ProjectileAnim.Time)
                             .SetEase(data.ProjectileAnim.Curve)
                             .SetDelay(data.ProjectileAnim.Delay)
                             .OnComplete(() =>
                                         {
                                             projectileSfx.stop(STOP_MODE.IMMEDIATE);
                                             Object.Destroy(projectileVfx.gameObject);
                                             ready = true;
                                         });
            }
            else
            {
                projectileVfx.transform.DOMove(hitPos, data.ProjectileAnim.Time)
                             .SetEase(data.ProjectileAnim.Ease)
                             .SetDelay(data.ProjectileAnim.Delay)
                             .OnComplete(() =>
                                         {
                                             projectileSfx.stop(STOP_MODE.IMMEDIATE);
                                             Object.Destroy(projectileVfx.gameObject);
                                             ready = true;
                                         });
            }
            
            yield return new WaitUntil(() => ready);

            onComplete?.Invoke();
        }

        private IEnumerator HitSequence(Action onComplete = null)
        {
            Vector3 hitPos = GetHitPos();
            
            if (data.HitAnim.Vfx)
            {
                Object.Instantiate(data.HitAnim.Vfx, hitPos, Quaternion.identity);
            }

            if (hitSfx.isValid())
            {
                hitSfx.start();
            }
            
            onComplete?.Invoke();
            yield return null;
        }

        protected virtual Vector3 GetHitPos()
        {
            return Vector3.zero;
        }
        
        public override string ToString()
        {
            return data.Name;
        }
    }
}
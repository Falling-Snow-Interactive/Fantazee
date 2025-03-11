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

            if (!data.CastSfx.IsNull)
            {
                castSfx = RuntimeManager.CreateInstance(data.CastSfx);
            }
            
            if (!data.ProjectileSfx.IsNull)
            {
                projectileSfx = RuntimeManager.CreateInstance(data.ProjectileSfx);
            }
            
            if (!data.HitSfx.IsNull)
            {
                hitSfx = RuntimeManager.CreateInstance(data.HitSfx);
            }
        }
        
        public void Cast(Damage damage, Action onComplete = null)
        {
            if (castSfx.isValid())
            {
                castSfx.start();
            }

            if (data.CastVfx)
            {
                Object.Instantiate(data.CastVfx, BattleController.Instance.Player.transform);
            }
            
            BattleController.Instance.StartCoroutine(SpellSequence(damage, onComplete));
        }

        protected abstract void Apply(Damage damage);

        private IEnumerator SpellSequence(Damage damage, Action onComplete = null)
        {
            yield return new WaitForSeconds(0.25f);
            
            if (data.HasCast)
            {
                bool ready = false;
                BattleController.Instance.StartCoroutine(CastSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            if (data.HasProjectile)
            {
                bool ready = false;
                BattleController.Instance.StartCoroutine(ProjectileSequence(() => ready = true));
                yield return new WaitUntil(() => ready);
            }

            Apply(damage);
            
            if (data.HasHit)
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
            GameObject projectileVfx = Object.Instantiate(data.ProjectileVfx, player.transform);
            projectileVfx.transform.localPosition = data.ProjectileSpawnOffset;
            projectileSfx.start();
            
            if (data.ProjectileEase is Ease.INTERNAL_Custom or Ease.INTERNAL_Zero)
            {
                projectileVfx.transform.DOMove(hitPos, data.ProjectileTime)
                             .SetEase(data.ProjectileCurve)
                             .SetDelay(data.ProjectileDelay)
                             .OnComplete(() =>
                                         {
                                             projectileSfx.stop(STOP_MODE.IMMEDIATE);
                                             Object.Destroy(projectileVfx.gameObject);
                                             ready = true;
                                         });
            }
            else
            {
                projectileVfx.transform.DOMove(hitPos, data.ProjectileTime)
                             .SetEase(data.ProjectileEase)
                             .SetDelay(data.ProjectileDelay)
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
            
            if (data.HitVfx)
            {
                Object.Instantiate(data.HitVfx, hitPos, Quaternion.identity);
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
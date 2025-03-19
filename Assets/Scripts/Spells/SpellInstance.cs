using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Characters.Player;
using Fantazee.Scores;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Object = UnityEngine.Object;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Fantazee.Spells
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
        
        public void Cast(ScoreResults scoreResults, Action onComplete = null)
        {
            if (castSfx.isValid())
            {
                castSfx.start();
            }

            if (data.CastAnim.CastVfx)
            {
                Object.Instantiate(data.CastAnim.CastVfx, BattleController.Instance.Player.transform);
            }
            
            BattleController.Instance.StartCoroutine(SpellSequence(scoreResults, onComplete));
        }

        protected abstract void Apply(ScoreResults scoreResults, Action onComplete);

        private IEnumerator SpellSequence(ScoreResults scoreResults, Action onComplete = null)
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
            
            if (data.HitAnim.HasHit)
            {
                ready = false;
                bool ready2 = false;
                BattleController.Instance.StartCoroutine(HitSequence(() => ready = true));
                Apply(scoreResults, () => ready2 = true);
                yield return new WaitUntil(() => ready && ready2);
            }
            else
            {
                ready = false;
                Apply(scoreResults, () => ready = true);
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
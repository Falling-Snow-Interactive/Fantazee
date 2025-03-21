using System;
using System.Collections;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Intentions;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Battle.Characters.Enemies.Actions.Instances
{
    public abstract class EnemyAction
    {
        public abstract ActionType Type { get; }
        public abstract Intention Intention { get; }
        
        public BattleEnemy Source { get; set; }

        private EnemyActionData data;

        public EnemyAction(EnemyActionData data, BattleEnemy source)
        {
            Source = source;
            this.data = data;
        }

        public abstract void Perform(Action onComplete);

        protected abstract Vector3 GetHitPos();
        
        protected IEnumerator CastSequence(Action onComplete = null)
        {
            Source.Visuals.Action();
            
            if (data.CastAnim.CastVfx)
            {
                GameObject.Instantiate(data.CastAnim.CastVfx, Source.transform);
            }

            if (!data.CastAnim.CastSfx.IsNull)
            {
                RuntimeManager.PlayOneShot(data.CastAnim.CastSfx);
            }
            
            yield return new WaitForSeconds(0.5f);
            
            onComplete?.Invoke();
        }

        protected void HitSequence()
        {
            Vector3 hitPos = GetHitPos();
            if (data.HitAnim.Vfx)
            {
                GameObject.Instantiate(data.HitAnim.Vfx, hitPos, Quaternion.identity);
            }

            if (!data.HitAnim.Sfx.IsNull)
            {
                RuntimeManager.PlayOneShot(data.HitAnim.Sfx);
            }
            
            BattleController.Instance.Player.Visuals.Hit();
        }
    }
}
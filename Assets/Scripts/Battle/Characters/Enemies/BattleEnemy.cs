using System;
using System.Collections;
using DG.Tweening;
using Fantazee.Enemies;
using Fantazee.Instance;
using Fantazee.StatusEffects;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay.Healths;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Battle.Characters.Enemies
{
    public class BattleEnemy : BattleCharacter
    {
        private EnemyData data;
        public EnemyData Data => data;
        
        // Health
        private Health health;
        public override Health Health => health;
        
        private Vector3 localRoot;
        
        // Audio
        protected override EventReference DeathSfxRef => data.DeathSfx;
        protected override EventReference EnterSfxRef => data.EnterSfx;

        private EventInstance attackSfx;

        #region Initialize

        public void Initialize(EnemyData data)
        {
            this.data = data;
            localRoot = transform.localPosition;

            int hp = data.Health * (GameInstance.Current.Environment.Index + 1);
            Debug.Log(hp);
            health = new Health(hp); // TODO - Real scaling
            attackSfx = RuntimeManager.CreateInstance(data.AttackSfx);
            SpawnVisuals(data.Visuals);
            base.Initialize();
            
            Debug.Log($"Enemy: {name} initialized");
        }

        #endregion
        
        #region Start Turn
        
        protected override void CharacterStartTurn()
        {
            Attack(EndTurn);
        }
        
        #endregion

        #region Attack
        
        private void Attack(Action onComplete)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete)
        {
            Visuals.Attack();
            attackSfx.start();
            yield return new WaitForSeconds(0.2f);
            BattleController.Instance.Player.Damage(data.Damage.Random() * (GameInstance.Current.Environment.Index + 1));
            if (data.StatusEffect != StatusEffectType.status_none 
                && Random.value <= data.StatusChance)
            {
                BattleController.Instance.Player.AddStatusEffect(data.StatusEffect, data.StatusTurns);
            }
            
            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Animations
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = localRoot;
                return;
            }
            
            transform.DOLocalMove(localRoot, data.ShowTime)
                     .SetEase(data.ShowEase)                     
                     .SetDelay(delay)
                     .OnPlay(() => RuntimeManager.PlayOneShot(EnterSfxRef))
                     .OnComplete(() =>
                                 {
                                     onComplete?.Invoke();
                                 });
        }

        public void Hide()
        {
            transform.localPosition = localRoot + data.HideOffset;
        }
        
        #endregion
        
        #region Gizmos
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (data)
            {
                Gizmos.DrawWireCube(transform.position, new Vector3(data.Size, 0.2f, 0));
            }
        }
        
        #endregion
    }
}

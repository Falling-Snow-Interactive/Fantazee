using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Characters.Intentions;
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
        
        // Intentions
        public event Action IntentionsUpdated;
        
        private List<Intention> intentions;
        public List<Intention> Intentions
        {
            get => intentions;
            set
            {
                intentions = value;
                IntentionsUpdated?.Invoke();
            }
        }
        
        // Audio
        protected override EventReference DeathSfxRef => data.DeathSfx;
        protected override EventReference EnterSfxRef => data.EnterSfx;

        private EventInstance attackSfx;

        #region Initialize

        public void Initialize(EnemyData data)
        {
            this.data = data;

            int hp = data.Health * (GameInstance.Current.Environment.Index + 1);
            health = new Health(hp); // TODO - Real scaling
            attackSfx = RuntimeManager.CreateInstance(data.AttackSfx);
            SpawnVisuals(data.Visuals);
            intentions = new List<Intention>();
            
            base.Initialize();
            
            Debug.Log($"Enemy: {name} initialized");
        }

        #endregion
        
        #region Intentions

        public void SetupIntentions()
        {
            List<Intention> intentions = new();
            int damage = data.Damage.Random() * (GameInstance.Current.Environment.Index + 1);
            Intention atkInt = new(IntentionType.intention_00_attack, damage);
            intentions.Add(atkInt);

            Intentions = intentions;
        }
        
        #endregion
        
        #region Turn

        public override void EndTurn()
        {
            Intentions.Clear();
            IntentionsUpdated?.Invoke();
            base.EndTurn();
        }

        protected override void CharacterStartTurn()
        {
            Queue<Intention> intentionQueue = new(Intentions);
            ProgressIntentions(intentionQueue);
        }

        private void ProgressIntentions(Queue<Intention> queue)
        {
            if (queue.Count <= 0)
            {
                EndTurn();
                return;
            }
            
            Intention intention = queue.Dequeue();
            switch (intention.Type)
            {
                case IntentionType.intention_00_attack:
                    Attack(intention, () => ProgressIntentions(queue));
                    break;
                case IntentionType.intention_01_defend:
                    Defend(intention, () => ProgressIntentions(queue));
                    break;
                case IntentionType.intention_02_healing:
                    Heal(intention, () => ProgressIntentions(queue));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        #endregion

        #region Attack
        
        private void Attack(Intention intention, Action onComplete)
        {
            StartCoroutine(AttackSequence(intention, onComplete));
        }

        private IEnumerator AttackSequence(Intention intention, Action onComplete)
        {
            Visuals.Attack();
            attackSfx.start();
            yield return new WaitForSeconds(0.2f);
            BattleController.Instance.Player.Damage(intention.Amount); // TODO do this properly.
            if (data.StatusEffect != StatusEffectType.status_none 
                && Random.value <= data.StatusChance)
            {
                BattleController.Instance.Player.AddStatusEffect(data.StatusEffect, data.StatusTurns);
            }
            
            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Attack
        
        private void Defend(Intention intention, Action onComplete)
        {
            StartCoroutine(DefendSequence(intention, onComplete));
        }

        private IEnumerator DefendSequence(Intention intention, Action onComplete)
        {
            Visuals.Action();
            attackSfx.start(); // TODO defend SFX
            yield return new WaitForSeconds(0.2f);
            Shield.Add(intention.Amount); // TODO do this properly.
            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Heal
        
        private void Heal(Intention intention, Action onComplete)
        {
            StartCoroutine(HealSequence(intention, onComplete));
        }

        private IEnumerator HealSequence(Intention intention, Action onComplete)
        {
            Visuals.Action();
            attackSfx.start(); // TODO - Heal SFX and VFX probably
            yield return new WaitForSeconds(0.2f);
            Health.Heal(intentions[0].Amount); // TODO do this properly.
            yield return new WaitForSeconds(0.5f);
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Animations
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                Visuals.transform.localPosition = Vector3.zero;
                return;
            }

            Visuals.transform.DOLocalMove(Vector3.zero, data.ShowTime)
                     .SetEase(data.ShowEase)                     
                     .SetDelay(delay)
                     .OnPlay(() => RuntimeManager.PlayOneShot(EnterSfxRef))
                     .OnComplete(() =>
                                 {
                                     onComplete?.Invoke();
                                 });
        }

        public void Hide(Action onComplete, bool force = false)
        {
            if (force)
            {
                Visuals.transform.localPosition = data.HideOffset;
                return;
            }
            
            Visuals.transform.DOLocalMove(data.HideOffset, data.ShowTime)
                   .SetEase(data.ShowEase)                     
                   .OnPlay(() => RuntimeManager.PlayOneShot(EnterSfxRef))
                   .OnComplete(() =>
                               {
                                   onComplete?.Invoke();
                               });
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

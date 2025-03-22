using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Characters.Enemies.Actions;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Enemies.Actions.Instances;
using Fantazee.Battle.Characters.Enemies.Actions.Randomizer;
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
        
        // Actions
        private Queue<EnemyAction> actions = new();
        private ActionRandomizer actionRandomizer;
        
        // Audio
        protected override EventReference DeathSfxRef => data.DeathSfx;
        protected override EventReference EnterSfxRef => data.EnterSfx;

        #region Initialize

        public void Initialize(EnemyData data)
        {
            this.data = data;

            int hp = data.Health * (GameInstance.Current.Environment.Index + 1);
            health = new Health(hp); // TODO - Real scaling
            SpawnVisuals(data.Visuals);
            intentions = new List<Intention>();

            actionRandomizer = new ActionRandomizer();
            foreach (ActionRandomizerEntry a in data.ActionRandomizer)
            {
                actionRandomizer.Add(a);
            }
            
            base.Initialize();
            
            Debug.Log($"Enemy: {name} initialized");
        }

        #endregion
        
        #region Turn
        
        public virtual void SetupActions()
        {
            actions = new Queue<EnemyAction>();
            List<Intention> intentions = new();

            int actionCount = data.ActionsPerTurn.Random();
            List<EnemyActionData> actionData = actionRandomizer.Randomize(actionCount, true);
            foreach (EnemyActionData ad in actionData)
            {
                EnemyAction action = ActionFactory.Create(ad, this);
                actions.Enqueue(action);
                intentions.Add(action.Intention);
            }

            Intentions = intentions;
        }

        public override void EndTurn()
        {
            Intentions.Clear();
            IntentionsUpdated?.Invoke();
            base.EndTurn();
        }

        protected override void CharacterStartTurn()
        {
            ProgressActions(actions);
        }

        private void ProgressActions(Queue<EnemyAction> queue)
        {
            if (queue.Count <= 0)
            {
                EndTurn();
                return;
            }
            
            EnemyAction action = queue.Dequeue();
            action.Perform(() => ProgressActions(queue));
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

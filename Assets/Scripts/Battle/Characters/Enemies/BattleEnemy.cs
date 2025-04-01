using System;
using DG.Tweening;
using Fantazee.Battle.Characters.Enemies.Actions;
using Fantazee.Battle.Characters.Enemies.Actions.ActionData;
using Fantazee.Battle.Characters.Enemies.Actions.Instances;
using Fantazee.Battle.Characters.Enemies.Actions.Randomizer;
using Fantazee.Battle.Characters.Intentions;
using Fantazee.Enemies;
using Fantazee.Instance;
using FMODUnity;
using Fsi.Gameplay.Healths;

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

        protected List<Intention> intentions;
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
        
        protected ActionRandomizer actionRandomizer;
        protected virtual ActionRandomizer ActionRandomizer => actionRandomizer;
        
        // Audio
        protected override EventReference DeathSfxRef => data.DeathSfx;
        protected override EventReference EnterSfxRef => data.EnterSfx;

        #region Initialize

        public virtual void Initialize(EnemyData data)
        {
            this.data = data;

            int hp = data.Health.Random() * (GameInstance.Current.Environment.Index + 1);
            health = new Health(hp); // TODO - Real scaling
            SpawnVisuals(data.Visuals);
            intentions = new List<Intention>();

            actionRandomizer = new ActionRandomizer();
            foreach (ActionRandomizerEntry a in data.ActionRandomizer)
            {
                actionRandomizer.Add(a);
            }
            
            base.Initialize();

            statusRoot.transform.localPosition = data.StatusBarPosition;
            
            Debug.Log($"Enemy: {name} initialized");
        }

        #endregion
        
        #region Turn
        
        public virtual void SetupTurnActions()
        {
            actions = new Queue<EnemyAction>();
            List<Intention> intentions = new();

            int actionCount = data.ActionsPerTurn.Randomize();
            List<EnemyActionData> actionData = ActionRandomizer.Randomize(actionCount, true);
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
                     .OnPlay(() =>
                             {
                                 if (!EnterSfxRef.IsNull)
                                 {
                                     RuntimeManager.PlayOneShot(EnterSfxRef);
                                 }
                             })
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
                Gizmos.DrawWireCube(transform.position + Vector3.up * data.Size.y / 2, data.Size);
            }
        }
        
        #endregion
    }
}

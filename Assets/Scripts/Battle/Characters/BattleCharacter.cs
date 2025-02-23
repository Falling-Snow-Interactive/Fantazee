using System;
using DG.Tweening;
using FMODUnity;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace Fantazee.Battle.Characters
{
    public abstract class BattleCharacter : MonoBehaviour, IDamageable
    {
        public event Action Damaged;

        public static event Action<BattleCharacter> Spawned;
        public static event Action<BattleCharacter> Despawned;
        
        [SerializeField]
        private Vector3 localRoot;
        
        [Header("Character")]
        
        [Header("Visuals")]

        [SerializeField]
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;

        [SerializeField]
        private float size = 1f;
        public float Size => size;
        
        [Header("Health")]

        [SerializeField]
        private HealthUi healthUi;
        
        public abstract Health Health { get; }
        
        [Header("Animations")]

        [Header("Hide/Show")]

        [SerializeField]
        private Vector3 hideOffset = Vector3.zero;

        [SerializeField]
        private float hideTime = 0.5f;

        [SerializeField]
        private Ease hideEase = Ease.Linear;

        [SerializeField]
        private float showTime = 0.5f;
        
        [SerializeField]
        private Ease showEase = Ease.Linear;
        
        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }
        
        public void Initialize()
        {
            Debug.Log($"Player - Initialize");
            localRoot = transform.localPosition;
            
            healthUi.Initialize(Health);
            Spawned?.Invoke(this);
        }
        
        public void Damage(int damage)
        {
            Debug.Log($"Enemy: Damage {damage}");
            Health.Damage(damage);
            visuals.Hit();

            if (Health.IsDead)
            {
                Destroy(gameObject);
            }
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = localRoot + hideOffset;
                return;
            }

            transform.DOLocalMove(localRoot + hideOffset, hideTime)
                     .SetEase(hideEase)
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                transform.localPosition = localRoot;
                return;
            }

            transform.DOLocalMove(localRoot, showTime)
                     .SetEase(showEase)                     
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(size, 0.2f, 0));
        }
    }
}
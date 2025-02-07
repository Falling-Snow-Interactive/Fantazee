using System;
using DG.Tweening;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace ProjectYahtzee.Battle.Characters
{
    public class GameplayCharacter : MonoBehaviour, IDamageable
    {
        public event Action Damaged;

        public static event Action<GameplayCharacter> Spawned;
        public static event Action<GameplayCharacter> Despawned;
        
        [SerializeField]
        private Vector3 localRoot;
        
        [Header("Character")]
        
        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;
        public CharacterVisuals Visuals => visuals;

        [SerializeField]
        private float size = 1f;
        public float Size => size;
        
        [Header("Health")]
        
        [SerializeField]
        private Health health;
        public Health Health => health;

        [SerializeField]
        private HealthUi healthUi;

        [Header("Hide/Show")]

        [SerializeField]
        private float hideOffset = 3f;

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
            localRoot = transform.localPosition;
            
            healthUi.Initialize(health);
            Spawned?.Invoke(this);
        }
        
        public void Damage(int damage)
        {
            health.Damage(damage);
            visuals.DoHit();

            if (health.IsDead)
            {
                Destroy(gameObject);
            }
        }

        public void Hide(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                Vector3 vector3 = transform.localPosition;
                vector3.x = hideOffset;
                transform.localPosition = vector3;
                return;
            }

            transform.DOLocalMoveX(hideOffset, hideTime)
                     .SetEase(hideEase)
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        public void Show(Action onComplete, float delay = 0, bool force = false)
        {
            if (force)
            {
                Vector3 vector3 = transform.localPosition;
                vector3.x = localRoot.x;
                transform.localPosition = vector3;
                return;
            }

            transform.DOLocalMoveX(localRoot.x, showTime)
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
using System;
using System.Collections;
using DG.Tweening;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace ProjectYahtzee.Battle.Characters.Enemies
{
    public class GameplayEnemy : MonoBehaviour, IDamageable
    {
        public event Action Damaged;

        public static event Action<GameplayEnemy> Spawned;
        public static event Action<GameplayEnemy> Despawned;
        
        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;

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

        [SerializeField]
        private Vector3 root;

        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }

        private void Awake()
        {
            
        }

        private void Start()
        {
            healthUi.Initialize(health);
            Spawned?.Invoke(this);
        }

        public void Initialize()
        {
            root = transform.localPosition;
        }

        public void Attack(Action onComplete)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete)
        {
            visuals.DoAttack();
            yield return new WaitForSeconds(1f);
            BattleController.Instance.Player.Damage(25);
            onComplete?.Invoke();
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
                vector3.x = root.x;
                transform.localPosition = vector3;
                return;
            }

            transform.DOLocalMoveX(root.x, showTime)
                     .SetEase(showEase)                     
                     .SetDelay(delay)
                     .OnComplete(() => onComplete?.Invoke());
        }
        
        #region Gizmos
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, new Vector3(size, 0.2f, 0));
        }
        
        #endregion
    }
}

using System;
using System.Collections;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using UnityEngine;

namespace ProjectYahtzee.Battle.Enemies
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

        private void OnDestroy()
        {
            Despawned?.Invoke(this);
        }

        private void Start()
        {
            healthUi.Initialize(health);
            Spawned?.Invoke(this);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, new Vector3(size, 0.2f, 0));
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
    }
}

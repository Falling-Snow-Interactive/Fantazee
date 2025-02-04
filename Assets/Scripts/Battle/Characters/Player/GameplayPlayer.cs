using System;
using System.Collections;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using ProjectYahtzee.Battle.Enemies;
using UnityEngine;

namespace ProjectYahtzee.Battle.Characters.Player
{
    public class GameplayPlayer : MonoBehaviour, IDamageable
    {
        public event Action Damaged;
        
        [Header("Health")]

        [SerializeField]
        private HealthUi healthUi;

        [Header("Visuals")]

        [SerializeField]
        private CharacterVisuals visuals;

        private void Start()
        {
            healthUi.Initialize(GameController.Instance.GameInstance.Health);
        }

        public void PerformAttack(GameplayEnemy enemy, Action onComplete = null)
        {
            StartCoroutine(AttackSequence(enemy, onComplete));
        }

        private IEnumerator AttackSequence(GameplayEnemy enemy, Action onComplete = null)
        {
            visuals.DoAttack();
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
        
        public void Damage(int damage)
        {
            GameController.Instance.GameInstance.Health.Damage(damage);
            visuals.DoHit();
        }
    }
}
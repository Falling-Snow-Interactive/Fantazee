using System;
using System.Collections;
using DG.Tweening;
using Fsi.Gameplay.Healths;
using Fsi.Gameplay.Healths.Ui;
using Fsi.Gameplay.Visuals;
using ProjectYahtzee.Battle.Characters.Enemies;
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

        private Vector3 root = Vector3.zero;

        private void Start()
        {
            root = transform.localPosition;
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
    }
}
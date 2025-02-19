using System;
using System.Collections;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantahzee.Battle.Characters.Enemies
{
    public class GameplayEnemy : GameplayCharacter
    {
        [Header("Health")]
        
        [SerializeField]
        private Health health;
        public override Health Health => health;

        #region Attack
        
        public void Attack(Action onComplete)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete)
        {
            Visuals.DoAttack();
            yield return new WaitForSeconds(1f);
            BattleController.Instance.Player.Damage(25);
            onComplete?.Invoke();
        }
        
        #endregion
        
        #region Gizmos
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            base.OnDrawGizmos();
        }
        
        #endregion
    }
}

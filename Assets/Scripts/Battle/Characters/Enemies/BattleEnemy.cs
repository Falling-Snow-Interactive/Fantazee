using System;
using System.Collections;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Battle.Characters.Enemies
{
    public class BattleEnemy : BattleCharacter
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
            Visuals.Attack();
            yield return new WaitForSeconds(0.2f);
            BattleController.Instance.Player.Damage(25); // TODO - <----
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

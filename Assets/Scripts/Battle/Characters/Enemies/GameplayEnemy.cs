using System;
using System.Collections;
using UnityEngine;

namespace ProjectYahtzee.Battle.Characters.Enemies
{
    public class GameplayEnemy : GameplayCharacter
    {
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

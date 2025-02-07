using System;
using System.Collections;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace ProjectYahtzee.Battle.Characters.Player
{
    public class GameplayPlayer : GameplayCharacter
    {
        public override Health Health => GameController.Instance.GameInstance.Health;

        public void PerformAttack(Action onComplete = null)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete = null)
        {
            Visuals.DoAttack();
            yield return new WaitForSeconds(1f);
            onComplete?.Invoke();
        }
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            base.OnDrawGizmos();
        }
    }
}
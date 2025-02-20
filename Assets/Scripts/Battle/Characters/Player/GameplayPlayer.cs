using System;
using System.Collections;
using System.Collections.Generic;
using Fantazhee.Boons.Handlers;
using FMODUnity;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.VFX;

namespace Fantazhee.Battle.Characters.Player
{
    public class GameplayPlayer : GameplayCharacter
    {
        public override Health Health => GameController.Instance.GameInstance.Health;

        [Header("Player")]

        [Header("Vfx")]

        [SerializeField]
        private VisualEffect bonusAttackVfx;
        
        [Header("Sfx")]
        
        [SerializeField]
        private EventReference attackSfx;
        public EventReference AttackSfx => attackSfx;
        
        [SerializeField]
        private EventReference attackHitSfx;
        public EventReference AttackHitSfx => attackHitSfx;

        [SerializeField]
        private EventReference bonusAttackSfx;
        
        // Boon handlers
        private List<IBoonDamageHandler> boonDamageHandlers = new();
        
        public void PerformAttack(Action onComplete = null)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete = null)
        {
            Visuals.DoAttack();
            yield return new WaitForSeconds(0.4f);
            RuntimeManager.PlayOneShot(attackSfx);
            yield return new WaitForSeconds(0.6f);
            onComplete?.Invoke();
        }
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            base.OnDrawGizmos();
        }
    }
}
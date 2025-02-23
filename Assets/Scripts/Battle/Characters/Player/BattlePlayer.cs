using System;
using System.Collections;
using FMODUnity;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.VFX;

namespace Fantazee.Battle.Characters.Player
{
    public class BattlePlayer : BattleCharacter
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
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            base.OnDrawGizmos();
        }
    }
}
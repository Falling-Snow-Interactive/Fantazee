using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using Fsi.Gameplay.Healths;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies
{
    public class BattleEnemy : BattleCharacter
    {
        [Header("Enemy")]
        
        [SerializeField]
        private RangeInt damage;
        
        [Header("Health")]
        
        [SerializeField]
        private Health health;
        public override Health Health => health;
        
        [Header("Rewards")]
        
        [SerializeField]
        private BattleRewards battleRewards;
        public BattleRewards BattleRewards => battleRewards;

        [Header("Audio")]

        [SerializeField]
        private EventReference attackSfxRef;
        private EventInstance attackSfx;

        protected override void Awake()
        {
            base.Awake();
            attackSfx = RuntimeManager.CreateInstance(attackSfxRef);
        }

        #region Attack
        
        public void Attack(Action onComplete)
        {
            StartCoroutine(AttackSequence(onComplete));
        }

        private IEnumerator AttackSequence(Action onComplete)
        {
            Visuals.Attack();
            attackSfx.start();
            yield return new WaitForSeconds(0.2f);
            BattleController.Instance.Player.Damage(damage.Random()); // TODO - <----
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

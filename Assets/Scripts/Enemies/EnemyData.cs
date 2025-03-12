using Fantazee.Battle;
using Fantazee.Battle.Characters;
using FMODUnity;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Enemies
{
    [CreateAssetMenu(menuName = "Enemies/Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Enemy")]

        [SerializeField]
        private int health;
        public int Health => health;
        
        [Header("Visuals")]

        [SerializeField]
        private GameplayCharacterVisuals visuals;
        public GameplayCharacterVisuals Visuals => visuals;

        [SerializeField]
        private float size = 1f;
        public float Size => size;
        
        [Header("Battle")]
        
        [SerializeField]
        private RangeInt damage;
        public RangeInt Damage => damage;
        
        [Header("Rewards")]
        
        [SerializeField]
        private BattleRewards battleRewards;
        public BattleRewards BattleRewards => battleRewards;
        
        [Header("Audio")]

        [SerializeField]
        private EventReference enterSfx;
        public EventReference EnterSfx => enterSfx;
        
        [SerializeField]
        private EventReference attackSfx;
        public EventReference AttackSfx => attackSfx;
        
        [SerializeField]
        private EventReference deathSfx;
        public EventReference DeathSfx => deathSfx;
    }
}
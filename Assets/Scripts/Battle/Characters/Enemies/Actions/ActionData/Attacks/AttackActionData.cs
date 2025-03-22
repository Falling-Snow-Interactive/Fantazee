using Fantazee.Battle.StatusEffects;
using Fantazee.Spells.Animations;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData.Attacks
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Attack")]
    public class AttackActionData : EnemyActionData
    {
        public override ActionType Type => ActionType.action_00_attack;
        
        [Header("Attack")]

        [SerializeField]
        private RangeInt damage = new(8, 12);
        public RangeInt Damage => damage;

        [SerializeField]
        private BattleStatusData statusEffect;
        public BattleStatusData StatusEffect => statusEffect;
        
        [Header("Battle Animations")]
        
        [SerializeField]
        private ProjectileAnimProp projectileAnim;
        public ProjectileAnimProp ProjectileAnim => projectileAnim;
    }
}
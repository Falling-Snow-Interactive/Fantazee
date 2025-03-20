using Fantazee.Battle.StatusEffects;
using Fantazee.Spells.Animations;
using Fantazee.Spells.Data.Animations;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData
{
    public abstract class AttackActionData : EnemyActionData
    {
        [Header("Attack")]

        [SerializeField]
        private RangeInt damage = new(8, 12);
        public RangeInt Damage => damage;

        [SerializeField]
        private BattleStatusData statusEffect;
        public BattleStatusData StatusEffect => statusEffect;
        
        [Header("Battle Animations")]
        
        [SerializeField]
        private CastAnimProp castAnim;
        public CastAnimProp CastAnim => castAnim;
        
        [SerializeField]
        private ProjectileAnimProp projectileAnim;
        public ProjectileAnimProp ProjectileAnim => projectileAnim;
        
        [SerializeField]
        private HitAnimProp hitAnim;
        public HitAnimProp HitAnim => hitAnim;
    }
}
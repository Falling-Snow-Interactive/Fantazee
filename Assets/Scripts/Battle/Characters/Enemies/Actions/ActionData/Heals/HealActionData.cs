using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData.Heals
{
    [CreateAssetMenu(menuName = "Enemies/Actions/action_02_heal", fileName = "action_02_heal_data")]
    public class HealActionData : EnemyActionData
    {
        public override ActionType Type => ActionType.action_02_heal;
        
        public enum TargetType
        {
            Self = 0,
            RandomEnemy = 1,
            AllEnemies = 2,
            FrontEnemy = 3,
        }
        
        [Header("Heal")]
        
        [SerializeField]
        private RangeInt amount;
        public RangeInt Amount => amount;
        
        [SerializeField]
        private TargetType target;
        public TargetType Target => target;
    }
}
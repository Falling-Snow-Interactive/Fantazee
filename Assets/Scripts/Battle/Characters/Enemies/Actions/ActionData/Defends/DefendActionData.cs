using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData.Defends
{
    [CreateAssetMenu(menuName = "Enemies/Actions/action_01_defend", fileName = "action_01_defend_data")]
    public class DefendActionData : EnemyActionData
    {
        public override ActionType Type => ActionType.action_01_defend;
        
        public enum TargetType
        {
            Self = 0,
            RandomEnemy = 1,
            AllEnemies = 2,
            FrontEnemy = 3,
        }
        
        [Header("Defend")]
        
        [SerializeField]
        private RangeInt amount;
        public RangeInt Amount => amount;
        
        [SerializeField]
        private TargetType target;
        public TargetType Target => target;
    }
}
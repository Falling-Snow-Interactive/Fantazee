
using System.Collections.Generic;
using Fantazee.Enemies;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Battle.Characters.Enemies.Actions.ActionData
{
    [CreateAssetMenu(menuName = "Enemies/Actions/Summon")]
    public class SummonActionData : EnemyActionData
    {
        public override ActionType Type => ActionType.action_03_summon;
        
        [Header("Summon")]

        [SerializeField]
        private List<EnemyData> summonPool = new();
        public List<EnemyData> SummonPool => summonPool;

        [SerializeField]
        private RangeInt summons = new(2, 3);
        public RangeInt Summons => summons;
    }
}

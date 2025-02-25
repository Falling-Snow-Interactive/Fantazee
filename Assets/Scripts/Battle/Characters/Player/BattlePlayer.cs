using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Battle.Characters.Player
{
    public class BattlePlayer : BattleCharacter
    {
        public override Health Health => GameController.Instance.GameInstance.Character.Health;
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            base.OnDrawGizmos();
        }
    }
}
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
        public override Health Health => GameController.Instance.GameInstance.Character.Health;
        
        protected override void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            base.OnDrawGizmos();
        }
    }
}
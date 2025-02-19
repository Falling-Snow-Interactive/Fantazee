using Fsi.Gameplay.Visuals;
using UnityEngine;
using UnityEngine.VFX;

namespace Fantahzee.Battle.Characters
{
    public class GameplayCharacterVisuals : CharacterVisuals
    {
        [Header("Hit")]

        [SerializeField]
        private VisualEffect hitEffect;
        
        public override void DoHit()
        {
            base.DoHit();
            hitEffect.Play();
        }
    }
}
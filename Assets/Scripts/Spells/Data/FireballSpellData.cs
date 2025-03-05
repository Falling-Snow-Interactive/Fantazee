using DG.Tweening;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Fireball")]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.Fireball;
        
        [Header("Fireball")]
        
        [Header("Transit")]

        [SerializeField]
        private GameObject tweenVfx;
        public GameObject TweenVfx => tweenVfx;
        
        [SerializeField]
        private EventReference tweenSfx;
        public EventReference TweenSfx => tweenSfx;

        [SerializeField]
        private Vector3 tweenVfxSpawnOffset;
        public Vector3 TweenVfxSpawnOffset => tweenVfxSpawnOffset;
        
        [SerializeField]
        private Vector3 tweenVfxHitOffset;
        public Vector3 TweenVfxHitOffset => tweenVfxHitOffset;

        [SerializeField]
        private Ease tweenEase = Ease.Linear;
        public Ease TweenEase => tweenEase;

        [SerializeField]
        private float tweenTime = 0.6f;
        public float TweenTime => tweenTime;

        [SerializeField]
        private float tweenDelay = 0.35f;
        public float TweenDelay => tweenDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
    }
}

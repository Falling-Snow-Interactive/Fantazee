using DG.Tweening;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Dagger")]
    public class DaggerData : SpellData
    {
        public override SpellType Type => SpellType.Dagger;
        
        [Header("Dagger")]
        
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
        private AnimationCurve tweenCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve TweenCurve => tweenCurve;

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
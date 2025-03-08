using DG.Tweening;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_00_Dagger", fileName = "Spell_00_Dagger", order = 0)]
    public class DaggerSpellData : SpellData
    {
        public override SpellType Type => SpellType.Dagger;
        
        [Header("Dagger")]
        
        [SerializeField]
        private GameObject daggerVfx;
        public GameObject DaggerVfx => daggerVfx;
        
        [SerializeField]
        private EventReference daggerSfx;
        public EventReference DaggerSfx => daggerSfx;

        [SerializeField]
        private Vector3 daggerSpawnOffset;
        public Vector3 DaggerSpawnOffset => daggerSpawnOffset;
        
        [SerializeField]
        private Vector3 daggerHitOffset;
        public Vector3 DaggerHitOffset => daggerHitOffset;

        [SerializeField]
        private Ease daggerEase = Ease.Linear;
        public Ease DaggerEase => daggerEase;
        
        [SerializeField]
        private AnimationCurve daggerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve DaggerCurve => daggerCurve;

        [SerializeField]
        private float daggerTime = 0.6f;
        public float DaggerTime => daggerTime;

        [SerializeField]
        private float daggerDelay = 0.0f;
        public float DaggerDelay => daggerDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
    }
}
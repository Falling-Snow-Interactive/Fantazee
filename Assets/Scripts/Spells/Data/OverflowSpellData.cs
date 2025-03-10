using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_05_Overflow", fileName = "Spell_05_Overflow", order = 5)]
    public class OverflowSpellData : SpellData
    {
        public override SpellType Type => SpellType.Overflow;
        
        [FormerlySerializedAs("daggerVfx")]
        [Header("Overflow")]
        
        [SerializeField]
        private GameObject overflowVfx;
        public GameObject OverflowVfx => overflowVfx;
        
        [FormerlySerializedAs("daggerSfx")]
        [SerializeField]
        private EventReference overflowSfx;
        public EventReference OverflowSfx => overflowSfx;

        [FormerlySerializedAs("daggerSpawnOffset")]
        [SerializeField]
        private Vector3 overflowSpawnOffset;
        public Vector3 OverflowSpawnOffset => overflowSpawnOffset;
        
        [FormerlySerializedAs("daggerHitOffset")]
        [SerializeField]
        private Vector3 overflowHitOffset;
        public Vector3 OverflowHitOffset => overflowHitOffset;

        [FormerlySerializedAs("daggerEase")]
        [SerializeField]
        private Ease overflowEase = Ease.Linear;
        public Ease OverflowEase => overflowEase;
        
        [FormerlySerializedAs("daggerCurve")]
        [SerializeField]
        private AnimationCurve overflowCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve OverflowCurve => overflowCurve;

        [FormerlySerializedAs("daggerTime")]
        [SerializeField]
        private float overflowTime = 0.6f;
        public float OverflowTime => overflowTime;

        [FormerlySerializedAs("daggerDelay")]
        [SerializeField]
        private float overflowDelay = 0.0f;
        public float OverflowDelay => overflowDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
    }
}

using DG.Tweening;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_04_Fireball", fileName = "Spell_04_Fireball", order = 4)]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.Fireball;

        [Header("Fireball")]

        [Range(0f, 2f)]
        [SerializeField]
        private float damageMod = 1f;
        public float DamageMod => damageMod;
        
        [SerializeField]
        private GameObject fireballVfx;
        public GameObject FireballVfx => fireballVfx;
        
        [SerializeField]
        private EventReference fireballSfx;
        public EventReference FireballSfx => fireballSfx;

        [SerializeField]
        private Vector3 fireballSpawnOffset;
        public Vector3 FireballSpawnOffset => fireballSpawnOffset;
        
        [SerializeField]
        private Vector3 fireballHitOffset;
        public Vector3 FireballHitOffset => fireballHitOffset;

        [SerializeField]
        private Ease fireballEase = Ease.Linear;
        public Ease FireballEase => fireballEase;
        
        [SerializeField]
        private AnimationCurve fireballCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve FireballCurve => fireballCurve;

        [SerializeField]
        private float fireballTime = 0.6f;
        public float FireballTime => fireballTime;

        [SerializeField]
        private float fireballDelay = 0.0f;
        public float FireballDelay => fireballDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
    }
}

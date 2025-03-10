using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_07_Push", fileName = "Spell_07_Push", order = 7)]
    public class PushSpellData : SpellData
    {
        public override SpellType Type => SpellType.Push;

        [Header("Push")]

        [Range(0, 2f)]
        [SerializeField]
        private float pushDamageMod;
        public float PushDamageMod => pushDamageMod;
        
        [Header("Animation Sequence")]
        
        [FormerlySerializedAs("daggerVfx")]
        [SerializeField]
        private GameObject pushVfx;
        public GameObject PushVfx => pushVfx;
        
        [FormerlySerializedAs("daggerSfx")]
        [SerializeField]
        private EventReference pushSfx;
        public EventReference PushSfx => pushSfx;

        [FormerlySerializedAs("daggerSpawnOffset")]
        [SerializeField]
        private Vector3 pushSpawnOffset;
        public Vector3 PushSpawnOffset => pushSpawnOffset;
        
        [FormerlySerializedAs("daggerHitOffset")]
        [SerializeField]
        private Vector3 pushHitOffset;
        public Vector3 PushHitOffset => pushHitOffset;

        [FormerlySerializedAs("daggerEase")]
        [SerializeField]
        private Ease pushEase = Ease.Linear;
        public Ease PushEase => pushEase;
        
        [FormerlySerializedAs("daggerCurve")]
        [SerializeField]
        private AnimationCurve pushCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve PushCurve => pushCurve;

        [FormerlySerializedAs("daggerTime")]
        [SerializeField]
        private float pushTime = 0.6f;
        public float PushTime => pushTime;

        [FormerlySerializedAs("daggerDelay")]
        [SerializeField]
        private float pushDelay = 0.0f;
        public float PushDelay => pushDelay;
        
        [Header("Hit")]

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
        
        [Header("Move")]
        
        [SerializeField]
        private float moveTime = 0.2f;
        public float MoveTime => moveTime;
        
        [SerializeField]
        private Ease moveEase = Ease.Linear;
        public Ease MoveEase => moveEase;

        [SerializeField]
        private float moveDelay = 0.0f;
        public float MoveDelay => moveDelay;

        protected override Dictionary<string, string> GetDescArgs()
        {
            float percent = pushDamageMod * 100f;
            
            Dictionary<string, string> args = base.GetDescArgs();
            args.Add("Percent", $"{percent}");
            return args;
        }
    }
}
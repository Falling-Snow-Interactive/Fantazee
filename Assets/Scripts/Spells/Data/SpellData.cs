using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Currencies;
using FMODUnity;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    public abstract class SpellData : ScriptableObject
    {
        public abstract SpellType Type { get; }
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDesc;
        public string Description
        {
            get
            {
                args ??= GetDescArgs();
                return locDesc.GetLocalizedString(args);
            }
        }

        private Dictionary<string, string> args;

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        [Header("Shop")]

        [SerializeField]
        private Currency cost = new(CurrencyType.Gold, 10);
        public Currency Cost => cost;

        [Header("Battle Animations")]

        [Header("Cast")]

        [SerializeField]
        private bool hasCast = true;
        public bool HasCast => hasCast;
        
        [SerializeField]
        private GameObject castVfx;
        public GameObject CastVfx => castVfx;

        [SerializeField]
        private EventReference castSfx;
        public EventReference CastSfx => castSfx;
        
        [Header("Projectile")]
        
        [SerializeField]
        private bool hasProjectile = true;
        public bool HasProjectile => hasProjectile;
        
        [FormerlySerializedAs("daggerVfx")]
        [SerializeField]
        private GameObject projectileVfx;
        public GameObject ProjectileVfx => projectileVfx;
        
        [FormerlySerializedAs("daggerSfx")]
        [SerializeField]
        private EventReference projectileSfx;
        public EventReference ProjectileSfx => projectileSfx;

        [FormerlySerializedAs("daggerSpawnOffset")]
        [SerializeField]
        private Vector3 projectileSpawnOffset;
        public Vector3 ProjectileSpawnOffset => projectileSpawnOffset;
        
        [FormerlySerializedAs("daggerHitOffset")]
        [SerializeField]
        private Vector3 projectileHitOffset;
        public Vector3 ProjectileHitOffset => projectileHitOffset;

        [FormerlySerializedAs("daggerEase")]
        [SerializeField]
        private Ease projectileEase = Ease.Linear;
        public Ease ProjectileEase => projectileEase;
        
        [FormerlySerializedAs("daggerCurve")]
        [SerializeField]
        private AnimationCurve projectileCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve ProjectileCurve => projectileCurve;

        [FormerlySerializedAs("daggerTime")]
        [SerializeField]
        private float projectileTime = 0.6f;
        public float ProjectileTime => projectileTime;

        [FormerlySerializedAs("daggerDelay")]
        [SerializeField]
        private float projectileDelay = 0.0f;
        public float ProjectileDelay => projectileDelay;
        
        [Header("Hit")]
        
        [SerializeField]
        private bool hasHit = true;
        public bool HasHit => hasHit;

        [SerializeField]
        private GameObject hitVfx;
        public GameObject HitVfx => hitVfx;

        [SerializeField]
        private EventReference hitSfx;
        public EventReference HitSfx => hitSfx;
        
        protected virtual Dictionary<string, string> GetDescArgs()
        {
            return new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
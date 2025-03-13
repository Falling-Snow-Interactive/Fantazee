using System;
using DG.Tweening;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Animations
{
    [Serializable]
    public class ProjectileAnimProp
    {
        [SerializeField]
        private bool hasProjectile = true;
        public bool HasProjectile => hasProjectile;
        
        [SerializeField]
        private GameObject vfx;
        public GameObject Vfx => vfx;
        
        [SerializeField]
        private EventReference sfx;
        public EventReference Sfx => sfx;

        [SerializeField]
        private Vector3 spawnOffset;
        public Vector3 SpawnOffset => spawnOffset;

        [SerializeField]
        private Ease ease = Ease.Linear;
        public Ease Ease => ease;
        
        [SerializeField]
        private AnimationCurve curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        public AnimationCurve Curve => curve;

        [SerializeField]
        private float time = 0.6f;
        public float Time => time;

        [SerializeField]
        private float delay = 0.0f;
        public float Delay => delay;
    }
}
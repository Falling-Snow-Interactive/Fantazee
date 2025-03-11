using System;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Animations
{
    [Serializable]
    public class HitAnimProp
    {
        [SerializeField]
        private bool hasHit = true;
        public bool HasHit => hasHit;

        [SerializeField]
        private GameObject vfx;
        public GameObject Vfx => vfx;

        [SerializeField]
        private EventReference sfx;
        public EventReference Sfx => sfx;

        [SerializeField]
        private Vector3 offset;
        public Vector3 Offset => offset;
    }
}
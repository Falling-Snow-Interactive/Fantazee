using System;
using FMODUnity;
using UnityEngine;

namespace Fantazee.Spells.Data.Animations
{
    [Serializable]
    public class CastAnimProp
    {
        [SerializeField]
        private bool hasCast = true;
        public bool HasCast => hasCast;
        
        [SerializeField]
        private GameObject castVfx;
        public GameObject CastVfx => castVfx;

        [SerializeField]
        private EventReference castSfx;
        public EventReference CastSfx => castSfx;
    }
}
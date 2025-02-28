using System;
using Fantazee.Battle.Environments;
using FMODUnity;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazee.Environments.Information
{
    [Serializable]
    public class EnvironmentInformation : Information<EnvironmentType>
    {
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        [Header("Audio")]

        [SerializeField]
        private EventReference musicReference;
        public EventReference MusicReference => musicReference;
    }
}
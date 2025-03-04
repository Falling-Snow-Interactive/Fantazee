using System;
using FMODUnity;
using fsi.settings.Informations;
using UnityEngine;

namespace Fantazee.Audio.Information
{
    [Serializable]
    public class MusicInformation : Information<MusicId>
    {
        [SerializeField]
        private EventReference music;
        public EventReference Music => music;
    }
}
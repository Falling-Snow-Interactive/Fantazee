using System;
using Fantazee.Audio;
using FMODUnity;
using Fsi.Gameplay.SceneManagement;
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

        [Header("Scene")]

        [SerializeField]
        private FsiSceneEntry scene;
        public FsiSceneEntry Scene => scene;

        [Header("Audio")]

        [SerializeField]
        private EventReference musicReference;
        public EventReference MusicReference => musicReference;

        [SerializeField]
        private MusicId mapMusicId;
        public MusicId MapMusicId => mapMusicId;

        [SerializeField]
        private MusicId battleMusicId;
        public MusicId BattleMusicId => battleMusicId;
    }
}
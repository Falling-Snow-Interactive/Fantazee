using System;
using Fantazee.Audio;
using FMODUnity;
using Fsi.Gameplay.SceneManagement;
using fsi.settings.Informations;

namespace Fantazee.Environments.Information
{
    [Serializable]
    public class EnvironmentInformation : Information<EnvironmentType>
    {
        [Header("Scene")]

        [SerializeField]
        private FsiSceneEntry scene;
        public FsiSceneEntry Scene => scene;

        [Header("Visuals")]
        
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        [SerializeField]
        private List<Sprite> backgrounds;

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
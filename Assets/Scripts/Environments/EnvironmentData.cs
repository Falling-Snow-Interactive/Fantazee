using Fantazee.Audio;
using FMODUnity;
using Fsi.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Environments
{
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "Environment/Data")]
    public class EnvironmentData : ScriptableObject
    {
        [SerializeField]
        private EnvironmentType type;
        public EnvironmentType Type => type;

        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "env_no_loc" : locName.GetLocalizedString();

        [SerializeField]
        private LocalizedString locDesc;
        public string Description => locDesc.IsEmpty ? "env_no_loc" : locDesc.GetLocalizedString();

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private Color color = Color.black;
        public Color Color => color;

        [Header("Scene")]

        [SerializeField]
        private FsiSceneEntry map;
        public FsiSceneEntry Map => map;

        [SerializeField]
        private FsiSceneEntry battle;
        public FsiSceneEntry Battle => battle;

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

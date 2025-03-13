using System.Collections.Generic;
using Fantazee.Audio;
using Fantazee.Encounters;
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

        [SerializeField]
        private List<Sprite> backgrounds;
        public List<Sprite> Backgrounds => backgrounds;

        [Header("Scene")]

        [SerializeField]
        private FsiSceneEntry map;
        public FsiSceneEntry Map => map;

        [SerializeField]
        private FsiSceneEntry battle;
        public FsiSceneEntry Battle => battle;

        [Header("Encounters")]

        [SerializeField]
        private List<EncounterData> encounters = new();

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
        
        public Sprite GetBackground()
        {
            return backgrounds[Random.Range(0, backgrounds.Count)];
        }

        public EncounterData GetEncounter()
        {
            return encounters[Random.Range(0, encounters.Count)];
        }
    }
}

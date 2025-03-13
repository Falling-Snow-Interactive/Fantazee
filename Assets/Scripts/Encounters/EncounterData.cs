using System.Collections.Generic;
using Fantazee.Encounters.Responses;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Encounters
{
    [CreateAssetMenu(fileName = "ENC_", menuName = "Encounters/Data", order = 0)]
    public class EncounterData : ScriptableObject
    {
        [SerializeField]
        private EncounterType type;
        public EncounterType Type => type;

        [Header("Localization")]

        [SerializeField]
        private new LocalizedString name;
        public LocalizedString Name => name;

        [SerializeField]
        private LocalizedString description;
        public string Description => description.GetLocalizedString();

        [SerializeField]
        private List<EncounterResponse> responses = new();
        public List<EncounterResponse> Responses => responses;
    }
}
using System.Collections.Generic;
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
        private LocalizedString title;
        public string Title => title.GetLocalizedString();

        [SerializeField]
        private LocalizedString body;
        public string Body => body.GetLocalizedString();

        [SerializeField]
        private List<EncounterResponse> responses = new();
        public List<EncounterResponse> Responses => responses;
    }
}
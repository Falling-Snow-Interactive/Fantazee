using Fantazee.Battle;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Encounters.Responses
{
    public class EncounterResponse : ScriptableObject
    {
        [SerializeField]
        private LocalizedString response;
        public string Response => response.GetLocalizedString();
    }
}

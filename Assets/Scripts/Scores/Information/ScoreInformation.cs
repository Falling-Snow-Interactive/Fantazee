using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Scores.Information
{
    [Serializable]
    public class ScoreInformation : Information<ScoreType>
    {
        [SerializeField]
        private LocalizedString locName;
        public LocalizedString LocName => locName;

        [SerializeField]
        private LocalizedString locDesc;
        public LocalizedString LocDesc => locDesc;
    }
}
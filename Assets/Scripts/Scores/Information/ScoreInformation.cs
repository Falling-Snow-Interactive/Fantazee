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

        [SerializeField]
        private float baseValue = 10;
        public float BaseValue => baseValue;

        [SerializeField]
        private float baseMod = 2;
        public float BaseMod => baseMod;
    }
}
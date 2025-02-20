using System;
using Fantazee.Currencies;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Boons.Information
{
    [Serializable]
    public class BoonInformation : Information<BoonType>
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public LocalizedString LocName => locName;
        
        [SerializeField]
        private LocalizedString locDescription;
        public LocalizedString LocDescription => locDescription;
        
        [Header("Visuals")]
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [Header("Shop")]

        [SerializeField]
        private Currency cost;
        public Currency Cost => cost;
    }
}
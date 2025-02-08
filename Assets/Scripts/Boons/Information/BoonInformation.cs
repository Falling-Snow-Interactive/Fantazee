using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace ProjectYahtzee.Boons.Information
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
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
    }
}
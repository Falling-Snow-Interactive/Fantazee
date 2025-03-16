using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Currencies.Information
{
    [Serializable]
    public class CurrencyInformation : Information<CurrencyType>
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no_loc" : locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDesc;
        public string Description => locDesc.IsEmpty ? "no_loc" : locDesc.GetLocalizedString();

        [Header("Visuals")]

        [SerializeField]
        private Color color;
        public Color Color => color;
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
    }
}
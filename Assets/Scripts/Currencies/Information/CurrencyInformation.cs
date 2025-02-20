using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazhee.Currencies.Information
{
    [Serializable]
    public class CurrencyInformation : Information<CurrencyType>
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public LocalizedString LocName => locName;
        
        [SerializeField]
        private LocalizedString locDesc;
        public LocalizedString LocDesc => locDesc;

        [Header("Visuals")]

        [SerializeField]
        private Color color;
        public Color Color => color;
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
    }
}
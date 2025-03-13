using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Npcs.Information
{
    [Serializable]
    public class NpcInformation : Information<NpcType>
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description => locDescription.GetLocalizedString();
    }
}
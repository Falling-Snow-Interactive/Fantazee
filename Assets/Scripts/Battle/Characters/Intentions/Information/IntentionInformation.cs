using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Battle.Characters.Intentions.Information
{
    [Serializable]
    public class IntentionInformation : Information<IntentionType>
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no_loc" : locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description => locDescription.IsEmpty ? "no_loc" : locDescription.GetLocalizedString();
        
        [Header("Visuals")]
        
        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;
        
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;
    }
}
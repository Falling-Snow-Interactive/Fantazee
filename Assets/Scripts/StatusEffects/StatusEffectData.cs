using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.StatusEffects
{
    public abstract class StatusEffectData : ScriptableObject
    {
        public abstract StatusEffectType Type { get; }
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no_loc" : locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDescription;
        public string Description
        {
            get
            {
                if (locDescription.IsEmpty)
                {
                    return "no_loc";
                }

                descArgs ??= BuildDescArgs();
                
                return locDescription.GetLocalizedString();
            }
        }
        private Dictionary<string,string> descArgs;

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        protected virtual Dictionary<string, string> BuildDescArgs()
        {
            Dictionary<string,string> args = new Dictionary<string, string>();
            return args;
        }
    }
}
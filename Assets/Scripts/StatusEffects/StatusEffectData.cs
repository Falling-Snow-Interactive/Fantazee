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

                descArgs ??= GetDescArgs();
                
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

        public Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new()
                                              {
                                                  { "StatusType", Type.ToString() },
                                                  { "StatusColor", ColorUtility.ToHtmlStringRGB(Color) },
                                              };
            return args;
        }
    }
}
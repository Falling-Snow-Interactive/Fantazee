using Fantazee.Currencies;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Relics.Data
{
    public abstract class RelicData : ScriptableObject
    {
        public abstract RelicType Type { get; }
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.IsEmpty ? "no-loc" : locName.GetLocalizedString();

        [SerializeField]
        private LocalizedString locDesc;
        public string Description => locDesc.IsEmpty ? "no-loc" : locDesc.GetLocalizedString();

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private Color color;
        public Color Color => color;

        [Header("Shop")]

        [SerializeField]
        private Currency cost;
        public Currency Cost => cost;
        
        public override string ToString() => Type.ToString();
    }
}
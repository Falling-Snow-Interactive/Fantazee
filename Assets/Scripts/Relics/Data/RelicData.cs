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
        public LocalizedString LocName => locName;

        [SerializeField]
        private LocalizedString locDesc;
        public LocalizedString LocDesc => locDesc;

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
    }
}
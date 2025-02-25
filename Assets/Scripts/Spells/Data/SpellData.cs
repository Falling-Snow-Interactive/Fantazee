using UnityEngine;
using UnityEngine.Localization;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Spells.Data
{
    public abstract class SpellData : ScriptableObject
    {
        public abstract SpellType Type { get; }
        
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
        private Color color = Color.white;
        public Color Color => color;

        [Header("Shop")]

        [SerializeField]
        private RangeInt cost = new(10, 15);
        public RangeInt Cost => cost;
    }
}
using UnityEngine;
using UnityEngine.Localization;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Spells
{
    [CreateAssetMenu(menuName = "Spells/Data")]
    public class SpellData : ScriptableObject
    {
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public LocalizedString Name => locName;

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
        private RangeInt cost = new(10, 15);
        public RangeInt Cost => cost;
    }
}
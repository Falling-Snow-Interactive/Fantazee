using UnityEngine;
using UnityEngine.Localization;
using RangeInt = Fsi.Gameplay.RangeInt;

namespace Fantazee.Relics
{
    [CreateAssetMenu(fileName = "RelicData", menuName = "Relics/Data")]
    public class RelicData : ScriptableObject
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
        private RangeInt cost = new(100, 150);
        public RangeInt Cost => cost;
    }
}
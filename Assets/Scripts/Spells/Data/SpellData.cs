using System.Collections.Generic;
using Fantazee.Currencies;
using FMODUnity;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Spells.Data
{
    public abstract class SpellData : ScriptableObject
    {
        public abstract SpellType Type { get; }
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();
        
        [SerializeField]
        private LocalizedString locDesc;
        public string Description
        {
            get
            {
                args ??= GetDescArgs();
                return locDesc.GetLocalizedString(args);
            }
        }

        private Dictionary<string, string> args;

        [Header("Visuals")]

        [SerializeField]
        private Sprite icon;
        public Sprite Icon => icon;

        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        [Header("Shop")]

        [SerializeField]
        private Currency cost = new(CurrencyType.Gold, 10);
        public Currency Cost => cost;

        [Header("Cast")]

        [SerializeField]
        private GameObject castVfx;
        public GameObject CastVfx => castVfx;

        [SerializeField]
        private EventReference castSfx;
        public EventReference CastSfx => castSfx;
        
        protected virtual Dictionary<string, string> GetDescArgs()
        {
            return new Dictionary<string, string>();
        }

        public override string ToString()
        {
            return name;
        }
    }
}
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Currencies;
using FMODUnity;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.VFX;
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
        public string Desc
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
    }
}
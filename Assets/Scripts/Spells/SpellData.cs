using System.Collections.Generic;
using Fantazee.Battle.StatusEffects;
using Fantazee.Currencies;
using Fantazee.Spells.Animations;
using Fantazee.Spells.Data.Animations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Spells
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
        private Currency cost = new(CurrencyType.currency_00_gold, 10);
        public Currency Cost => cost;

        [Header("Battle Animations")]
        
        [SerializeField]
        private CastAnimProp castAnim;
        public CastAnimProp CastAnim => castAnim;
        
        [SerializeField]
        private ProjectileAnimProp projectileAnim;
        public ProjectileAnimProp ProjectileAnim => projectileAnim;
        
        [SerializeField]
        private HitAnimProp hitAnim;
        public HitAnimProp HitAnim => hitAnim;
        
        protected virtual Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new();
            return args;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
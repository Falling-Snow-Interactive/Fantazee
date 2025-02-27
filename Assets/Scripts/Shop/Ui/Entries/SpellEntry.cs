using System;
using Fantazee.Currencies;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class SpellEntry : ShopEntryUi
    {
        private Action<SpellEntry> onSelected;
        
        [SerializeReference]
        private SpellType spell;
        public SpellType Spell => spell;

        [SerializeField]
        private Currency cost;

        [SerializeField]
        private Image icon;
        
        public void Initialize(SpellType spell, Action<SpellEntry> onSelected)
        {
            this.spell = spell;
            this.onSelected = onSelected;

            if (SpellSettings.Settings.TryGetSpell(spell, out SpellData data))
            {
                cost = new Currency(CurrencyType.Gold, data.Cost.Random());
                ShowEntry(data.LocName.GetLocalizedString(), data.LocDesc.GetLocalizedString(), cost);
                icon.sprite = data.Icon;
            }
        }
        
        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
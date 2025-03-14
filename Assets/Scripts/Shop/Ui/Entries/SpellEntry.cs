using System;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using Fantazee.Spells.Ui;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Entries
{
    public class SpellEntry : ShopEntryUi
    {
        private Action<SpellEntry> onSelected;

        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [FormerlySerializedAs("spellEntry")]
        [SerializeField]
        private SpellButton spellButtonEntry;
        
        public void Initialize(SpellInstance spell, Action<SpellEntry> onSelected)
        {
            this.onSelected = onSelected;
            this.spell = spell;
            
            spellButtonEntry.Initialize(spell);
            ShowEntry(spell.Data.Name, spell.Data.Description, spell.Data.Cost);
        }
        
        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
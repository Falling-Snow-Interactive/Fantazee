using System;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Spells.Data;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class SpellEntry : ShopEntryUi
    {
        private Action<SpellEntry> onSelected;

        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [SerializeField]
        private ScoreEntrySpell spellEntry;
        
        public void Initialize(SpellInstance spell, Action<SpellEntry> onSelected)
        {
            this.onSelected = onSelected;
            this.spell = spell;
            
            spellEntry.Initialize(0, spell);
            ShowEntry(spell.Data.Name, spell.Data.Description, spell.Data.Cost);
        }
        
        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
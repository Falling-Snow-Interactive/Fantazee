using System;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Spells.Data;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class SpellEntry : ShopEntryUi
    {
        private Action<SpellEntry> onSelected;

        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        [SerializeField]
        private ScoreEntrySpell spellEntry;
        
        public void Initialize(SpellData data, Action<SpellEntry> onSelected)
        {
            this.onSelected = onSelected;
            this.data = data;
            
            spellEntry.Initialize(0, data.Type);
            ShowEntry(data.LocName.GetLocalizedString(), data.LocDesc.GetLocalizedString(), data.Cost);
        }
        
        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
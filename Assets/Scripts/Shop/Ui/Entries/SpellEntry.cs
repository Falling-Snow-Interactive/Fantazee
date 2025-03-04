using System;
using Fantazee.Spells.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class SpellEntry : ShopEntryUi
    {
        private Action<SpellEntry> onSelected;

        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        [SerializeField]
        private Image icon;
        
        public void Initialize(SpellData data, Action<SpellEntry> onSelected)
        {
            this.onSelected = onSelected;
            this.data = data;
            
            icon.sprite = data.Icon;
            ShowEntry(data.LocName.GetLocalizedString(), data.LocDesc.GetLocalizedString(), data.Cost);
        }
        
        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
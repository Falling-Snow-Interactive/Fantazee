using System;
using Fantazee.Currencies;
using Fantazee.Relics;
using Fantazee.Relics.Ui;
using UnityEngine;

namespace Fantazee.Shop.Ui.Entries
{
    public class RelicEntry : ShopEntryUi
    {
        private Action<RelicEntry> onSelected;
        
        public RelicInstance Relic { get; private set; }

        [SerializeField]
        private RelicEntryUi relicEntryUi;
        
        public void Initialize(RelicInstance relic, Action<RelicEntry> onSelected)
        {
            this.onSelected = onSelected;
            Relic = relic;
            
            relicEntryUi.Initialize(relic);
            
            string name = relic.Data.LocName.GetLocalizedString();
            string desc = relic.Data.LocName.GetLocalizedString();
            Currency cost = relic.Data.Cost;
            ShowEntry(name, desc, cost);
        }

        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
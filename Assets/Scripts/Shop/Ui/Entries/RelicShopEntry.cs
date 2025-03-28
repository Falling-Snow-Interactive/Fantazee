using System;
using Fantazee.Currencies.Ui;
using Fantazee.Relics.Instance;
using Fantazee.Relics.Ui;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class RelicShopEntry : RelicEntryUi
    {
        private Action<RelicShopEntry> onSelected;
        
        public RelicInstance Relic { get; private set; }
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        public void Initialize(RelicInstance relic, Action<RelicShopEntry> onSelected)
        {
            base.Initialize(relic);
            this.onSelected = onSelected;
            Relic = relic;

            currencyEntryUi.SetCurrency(relic.Data.Cost);
        }

        public override void OnClick()
        {
            base.OnClick();
            onSelected?.Invoke(this);
        }
    }
}
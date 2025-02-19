using System;
using ProjectYahtzee.Boons;
using ProjectYahtzee.Boons.Settings;
using ProjectYahtzee.Currencies;

namespace ProjectYahtzee.Shop.Ui.Entries
{
    public class BoonEntry : ShopEntryUi
    {
        private Action<BoonEntry> onSelected;
        
        public BoonType Boon { get; private set; }
        
        public void Initialize(BoonType boon, Action<BoonEntry> onSelected)
        {
            Boon = boon;

            if (BoonSettings.Settings.Information.TryGetInformation(boon, out var information))
            {
                string name = information.LocName.GetLocalizedString();
                string desc = information.LocDescription.GetLocalizedString();
                Currency cost = information.Cost;
                ShowEntry(name, desc, cost);
            }

            this.onSelected = onSelected;
        }

        public override void OnEntrySelected()
        {
            onSelected?.Invoke(this);
        }
    }
}
using Fantahzee.Currencies;

namespace Fantahzee.Shop.Ui.Entries
{
    public class RelicEntry : ShopEntryUi
    {
        public void Initialize(/*Relic relic*/)
        {
            string name = "relic"; // relic.Information.LocName.GetLocalizedString();
            string desc = "relic"; // relic.Information.LocName.GetLocalizedString();
            Currency cost = new Currency(CurrencyType.Gold, 100); // relic.Information.Cost;
            ShowEntry(name, desc, cost);
        }

        public override void OnEntrySelected()
        {
            
        }
    }
}
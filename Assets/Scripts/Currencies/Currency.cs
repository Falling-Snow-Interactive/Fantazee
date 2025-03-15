using System;
using Fantazee.Currencies.Information;
using Fantazee.Currencies.Settings;
using Fsi.Currencies;

namespace Fantazee.Currencies
{
    [Serializable]
    public class Currency : Currency<CurrencyType>
    {
        public Currency(CurrencyType type, int amount) : base(type, amount)
        {
        }

        public Currency() : base()
        {
        }
    }
}
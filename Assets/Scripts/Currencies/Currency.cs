using System;
using Fsi.Currencies;

namespace Fantahzee.Currencies
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
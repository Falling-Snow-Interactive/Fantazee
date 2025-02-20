using System;
using Fsi.Currencies;

namespace Fantazhee.Currencies
{
    [Serializable]
    public class Wallet : Wallet<CurrencyType, Currency>
    {
        public Wallet(CurrencyType type, int amount) : base(type, amount)
        {
            
        }
    }
}
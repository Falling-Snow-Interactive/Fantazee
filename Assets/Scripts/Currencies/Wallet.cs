using System;
using Fsi.Currencies;

namespace Fantazee.Currencies
{
    [Serializable]
    public class Wallet : Wallet<CurrencyType, Currency>
    {
        public Wallet(CurrencyType type, int amount) : base(type, amount) { }
        
        public Wallet(Wallet wallet) : base(wallet) { }
    }
}
using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.Maps.Ui
{
    public class MapUi : MbSingleton<MapUi>
    {
        [SerializeField]
        private CurrencyEntryUi currencyEntry;

        private void Start()
        {
            if (GameController.Instance.GameInstance.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency currency))
            {
                currencyEntry.SetCurrency(currency);
            }
        }
    }
}

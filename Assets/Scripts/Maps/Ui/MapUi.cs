using Fantazee.Currencies;
using Fantazee.Currencies.Ui;
using Fantazee.Instance;
using Fsi.Gameplay;
using Fsi.Gameplay.Healths.Ui;
using UnityEngine;

namespace Fantazee.Maps.Ui
{
    public class MapUi : MbSingleton<MapUi>
    {
        [SerializeField]
        private CurrencyEntryUi currencyEntry;
        
        [SerializeField]
        private HealthUi health;

        private void Start()
        {
            if (GameInstance.Current.Character.Wallet.TryGetCurrency(CurrencyType.Gold, out Currency currency))
            {
                currencyEntry.SetCurrency(currency);
            }
            
            health.Initialize(GameInstance.Current.Character.Health);
        }
    }
}

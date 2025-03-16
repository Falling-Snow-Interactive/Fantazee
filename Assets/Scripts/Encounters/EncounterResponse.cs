using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Currencies.Information;
using Fantazee.Currencies.Settings;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Encounters
{
    [CreateAssetMenu(fileName = "Enc_", menuName = "Encounters/Response")]
    public class EncounterResponse : ScriptableObject
    {
        [Header("Localization")]
        
        [SerializeField]
        private LocalizedString header;
        public string Header => header.GetLocalizedString();

        [SerializeField]
        private LocalizedString body;
        public string Body
        {
            get
            {
                bodyArgs = BuildDictionary();
                return body.GetLocalizedString(bodyArgs);
            }
        }
        private Dictionary<string, string> bodyArgs;

        [SerializeField]
        private LocalizedString result;
        public string Result => result.GetLocalizedString();

        [Header("Selection")]

        [SerializeField]
        private EncounterCost cost;
        public EncounterCost Cost => cost;
        
        [Space(10)]
        
        [SerializeField]
        private EncounterRewards rewards;
        public EncounterRewards Rewards => rewards;

        private Dictionary<string, string> BuildDictionary()
        {
            Dictionary<string, string> dictionary = new()
                                                    {
                                                        { "Hp", Cost.Health.current.ToString() },
                                                        { "MaxHp", Cost.Health.max.ToString() },
                                                    };

            foreach (Currency currency in Cost.Wallet.Currencies)
            {
                if (CurrencySettings.Settings.CurrencyInformation.TryGetInformation(currency.type,
                             out CurrencyInformation information))
                {
                    dictionary.Add($"{currency.type}", $"{currency.amount} {information.Name}");
                }
            }

            return dictionary;
        }
    }
}

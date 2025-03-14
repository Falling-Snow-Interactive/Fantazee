using System.Collections.Generic;
using Fantazee.Currencies;
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
        private EncounterSelection cost;
        public EncounterSelection Cost => cost;
        
        [Space(10)]
        
        [SerializeField]
        private EncounterSelection rewards;
        public EncounterSelection Rewards => rewards;

        private Dictionary<string, string> BuildDictionary()
        {
            Dictionary<string, string> dictionary = new()
                                                    {
                                                        { "Hp", Cost.Health.current.ToString() },
                                                        { "MaxHp", Cost.Health.max.ToString() },
                                                    };

            foreach (Currency currency in Cost.Wallet.Currencies)
            {
                dictionary.Add(currency.type.ToString(), currency.ToString());
            }

            return dictionary;
        }
    }
}

using System.Collections.Generic;
using Fantazee.Currencies;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Encounters
{
    [CreateAssetMenu(fileName = "EncounterResponse", menuName = "Encounters/Response")]
    public class EncounterResponse : ScriptableObject
    {
        [SerializeField]
        private LocalizedString header;
        public string Header => header.GetLocalizedString();

        [SerializeField]
        private LocalizedString body;
        public string Body
        {
            get
            {
                args = BuildDictionary();
                return body.GetLocalizedString(args);
            }
        }
        private Dictionary<string, string> args;

        [Header("Cost")]

        [SerializeField]
        private Health health;
        public Health Health => health;
        
        [SerializeField]
        private Wallet wallet;
         public Wallet Wallet => wallet;
         
         [Header("Result")]
         
         [SerializeField]
         private EncounterResult result;
         public EncounterResult Result => result;
        
        private Dictionary<string, string> BuildDictionary()
        {
            Dictionary<string, string> dictionary = new()
                                                    {
                                                        { "Hp", health.current.ToString() },
                                                        { "MaxHp", health.max.ToString() },
                                                    };

            foreach (Currency currency in wallet.Currencies)
            {
                dictionary.Add(currency.type.ToString(), currency.ToString());
            }
            
            return dictionary;
        }
    }
}

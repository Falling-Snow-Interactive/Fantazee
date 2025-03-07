using System.Collections.Generic;
using Fantazee.Currencies;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Scores.Data
{
    public abstract class ScoreData : ScriptableObject
    {
        public abstract ScoreType Type { get; }
        
        [Header("Localization")]

        [SerializeField]
        private LocalizedString locName;
        public string Name => locName.GetLocalizedString();

        [SerializeField]
        private LocalizedString locDesc;

        public string Description
        {
            get
            {
                args ??= GetDescArgs();
                return locDesc.GetLocalizedString(args);
                
            }
        }
        
        private Dictionary<string, string> args;
        
        [Header("Cost")]

        [SerializeField]
        private Currency cost;
        public Currency Cost => cost;

        protected virtual Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            return args;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
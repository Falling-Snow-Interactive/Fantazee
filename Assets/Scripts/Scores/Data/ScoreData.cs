using System.Collections.Generic;
using Fantazee.Currencies;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Scores.Data
{
    // [CreateAssetMenu(menuName = "Fantazee/Data")]
    public abstract class ScoreData : ScriptableObject
    {
        [Header("Score")]
        
        [SerializeField]
        private ScoreType type;
        public ScoreType Type => type;
        
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
        
        public abstract Dictionary<string, string> GetDescArgs();

        public override string ToString()
        {
            return name;
        }
    }
}
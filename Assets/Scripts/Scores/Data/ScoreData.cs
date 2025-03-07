using System.Collections.Generic;
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
        public string Description => locDesc.GetLocalizedString();

        public abstract Dictionary<string, string> GetDescArgs();
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Relics;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.Localization;

namespace Fantazee.Encounters
{
    [Serializable]
    public class EncounterResult
    {
        [Header("Localization")]
        
        [SerializeField]
        private LocalizedString body;
        public string Body => body.GetLocalizedString();
        
        [Header("Results")]
        
        [SerializeField]
        private Health health;
        public Health Health => health;
        
        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;
        
        [SerializeField]
        private List<RelicType> relics;
        public List<RelicType> Relics => relics;
    }
}
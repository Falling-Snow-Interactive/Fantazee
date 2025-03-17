using System;
using Fantazee.Currencies;
using Fsi.Gameplay.Healths;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Encounters
{
    [Serializable]
    public class EncounterCost
    {
        [SerializeField]
        private Health health = new(0);
        public Health Health => health;

        [SerializeField]
        private Wallet wallet = new();
        public Wallet Wallet => wallet;

        [SerializeField]
        private bool randomRelic;
        public bool RandomRelic => randomRelic;

        [SerializeField]
        private bool randomSpell;
        public bool RandomSpell => randomSpell;
    }
}
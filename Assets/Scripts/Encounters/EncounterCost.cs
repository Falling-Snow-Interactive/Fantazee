using System;
using Fantazee.Currencies;
using Fsi.Gameplay.Healths;
using UnityEngine;

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
        private bool relic;
        public bool Relic => relic;

        [SerializeField]
        private bool spell;
        public bool Spell => spell;
    }
}
using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Relics.Bucket;
using Fantazee.Spells;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Encounters
{
    [Serializable]
    public class EncounterRewards
    {
        [SerializeField]
        private Health health = new(0);
        public Health Health => health;

        [SerializeField]
        private Wallet wallet = new();
        public Wallet Wallet => wallet;

        [SerializeField]
        private RelicBucket relics = new();
        public RelicBucket Relics => relics;

        [SerializeField]
        private List<SpellType> spells = new();
        public List<SpellType> Spells => spells;
    }
}
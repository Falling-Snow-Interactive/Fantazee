using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Relics;
using Fantazee.Spells;
using Fsi.Gameplay.Healths;
using UnityEngine;

namespace Fantazee.Encounters
{
    [Serializable]
    public class EncounterSelection
    {
        [SerializeField]
        private Health health = new(0);
        public Health Health => health;

        [SerializeField]
        private Wallet wallet = new();
        public Wallet Wallet => wallet;

        [SerializeField]
        private List<RelicType> relics = new();
        public List<RelicType> Relics => relics;

        [SerializeField]
        private List<SpellType> spells = new();
        public List<SpellType> Spells => spells;
    }
}
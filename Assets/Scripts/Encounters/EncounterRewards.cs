using System;
using System.Collections.Generic;
using Fantazee.Currencies;
using Fantazee.Randomizers;
using Fantazee.Relics.Bucket;
using Fantazee.Relics.Data;
using Fantazee.Spells;
using Fantazee.Spells.Bucket;
using Fsi.Gameplay.Healths;
using UnityEngine;
using RangeInt = Fsi.Gameplay.RangeInt;

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
        private RelicBucket relics;

        [SerializeField]
        private IntRandomizer numberOfRelics;

        [SerializeField]
        private SpellBucket spells;

        [SerializeField]
        private IntRandomizer numberOfSpells;

        public List<RelicData> GetRelicRewards()
        {
            List<RelicData> relics = new();
            if (this.relics != null)
            {
                int number = numberOfRelics.Randomize();
                Debug.Log($"Getting {number} relics.");
                for (int i = 0; i < number; i++)
                {
                    relics.Add(this.relics.GetRandom());
                }
            }

            return relics;
        }

        public List<SpellData> GetSpellRewards()
        {
            List<SpellData> spells = new();
            if (this.spells != null)
            {
                int number = numberOfSpells.Randomize();
                Debug.Log($"Getting {number} spells.");
                for (int i = 0; i < number; i++)
                {
                    spells.Add(this.spells.GetRandom());
                }
            }
            return spells;
        }
    }
}
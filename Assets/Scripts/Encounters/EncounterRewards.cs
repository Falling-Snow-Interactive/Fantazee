using System;
using System.Collections.Generic;
using Fantazee.Currencies;
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
        private List<RelicData> relicRewards = null;
        public List<RelicData> Relics
        {
            get
            {
                if (relicRewards == null || relicRewards.Count == 0)
                {
                    relicRewards = BuildRelicRewards();
                }
                return relicRewards;
            }
        }

        [SerializeField]
        private RangeInt numberOfRelics = new(0,0);

        [SerializeField]
        private SpellBucket spells;
        private List<SpellData> spellRewards = null;
        public List<SpellData> Spells
        {
            get
            {
                if (spellRewards == null || spellRewards.Count == 0)
                {
                    spellRewards = BuildSpellRewards();
                }
                return spellRewards;
            }
        }

        [SerializeField]
        private RangeInt numberOfSpells = new(0,0);

        private List<RelicData> BuildRelicRewards()
        {
            List<RelicData> relics = new();
            for (int i = 0; i < numberOfRelics.Random() && this.relics != null; i++)
            {
                relics.Add(this.relics.GetRandom());
            }
            return relics;
        }
        
        private List<SpellData> BuildSpellRewards()
        {
            List<SpellData> spells = new();
            for (int i = 0; i < numberOfSpells.Random() && this.spells != null; i++)
            {
                spells.Add(this.spells.GetRandom());
            }
            return spells;
        }
    }
}
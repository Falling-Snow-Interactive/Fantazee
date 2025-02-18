using System;
using System.Collections.Generic;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace ProjectYahtzee.Items.Dice.Randomizer
{
    [Serializable]
    public class DieRandomizer : Randomizer<int, DieRandomizerEntry>
    {
        [SerializeField]
        private List<DieRandomizerEntry> entries;
        public override List<DieRandomizerEntry> Entries => entries;

        public DieRandomizer(List<DieRandomizerEntry> entries)
        {
            this.entries = entries;
        }

        public static DieRandomizer D6
        {
            get
            {
                List<DieRandomizerEntry> entries = new List<DieRandomizerEntry>();
                for (int i = 1; i <= 6; i++)
                {
                    DieRandomizerEntry entry = new DieRandomizerEntry
                                                {
                                                    Value = i,
                                                    Weight = 1
                                                };
                    
                    entries.Add(entry);
                }

                return new DieRandomizer(entries);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Dices.Randomizer
{
    [Serializable]
    public class DiceRandomizer : Randomizer<int, DiceRandomizerEntry>
    {
        [SerializeField]
        private List<DiceRandomizerEntry> entries;
        public override List<DiceRandomizerEntry> Entries => entries;

        public DiceRandomizer(List<DiceRandomizerEntry> entries)
        {
            this.entries = entries;
        }

        public static DiceRandomizer D6
        {
            get
            {
                List<DiceRandomizerEntry> entries = new List<DiceRandomizerEntry>();
                for (int i = 1; i <= 6; i++)
                {
                    DiceRandomizerEntry entry = new DiceRandomizerEntry
                                                {
                                                    Value = i,
                                                    Weight = 1
                                                };
                    
                    entries.Add(entry);
                }

                return new DiceRandomizer(entries);
            }
        }
    }
}
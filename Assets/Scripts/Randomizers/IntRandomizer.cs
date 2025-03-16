using System;
using System.Collections.Generic;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace Fantazee.Randomizers
{
    [Serializable]
    public class IntRandomizer : Randomizer<int, IntRandomizerEntry>
    {
        [SerializeField]
        private List<IntRandomizerEntry> entries = new();
        public override List<IntRandomizerEntry> Entries => entries;
    }
}
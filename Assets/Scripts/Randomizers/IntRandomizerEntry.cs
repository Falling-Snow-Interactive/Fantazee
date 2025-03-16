using System;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace Fantazee.Randomizers
{
    [Serializable]
    public class IntRandomizerEntry : RandomizerEntry<int>
    {
        [SerializeField]
        private int weight = 1;
        public override int Weight
        {
            get => weight;
            set => weight = value;
        }

        [SerializeField]
        private int value = 1;
        public override int Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
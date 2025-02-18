using System;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace ProjectYahtzee.Items.Dice.Randomizer
{
    [Serializable]
    public class DieRandomizerEntry : RandomizerEntry<int>
    {
        [SerializeField]
        private int weight = 1;

        public override int Weight
        {
            get => weight;
            set => weight = value;
        }
        
        [Range(1,6)]
        [SerializeField]
        private int value = 1;

        public override int Value
        {
            get => value;
            set => this.value = value;
        }
    }
}
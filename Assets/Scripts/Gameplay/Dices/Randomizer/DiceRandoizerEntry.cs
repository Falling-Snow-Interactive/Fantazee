using System;
using Fsi.Gameplay.Randomizers;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Dices.Randomizer
{
    [Serializable]
    public class DiceRandomizerEntry : RandomizerEntry<int>
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
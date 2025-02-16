using System;
using ProjectYahtzee.Items.Dice.Randomizer;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Items.Dice
{
    [Serializable]
    public class Die : Item
    {
        [SerializeField]
        private int value;

        public int Value
        {
            get => value;
            set => this.value = value;
        }
        
        [FormerlySerializedAs("diceRandomizer")]
        [SerializeField]
        private DieRandomizer dieRandomizer;

        public Die()
        {
            value = 6;
            dieRandomizer = DieRandomizer.D6;
        }

        public void Roll()
        {
            value = dieRandomizer.Randomize();
        }

        public override void Upgrade()
        {
            throw new NotImplementedException();
        }
    }
}
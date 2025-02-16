using System;
using ProjectYahtzee.Battle.Dice.Randomizer;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Battle.Dice
{
    [Serializable]
    public class Die
    {
        [SerializeField]
        private int value = 6;

        public int Value
        {
            get => value;
            set => this.value = value;
        }

        [SerializeField]
        private bool locked;
        public bool Locked
        {
            get => locked;
            set => locked = value;
        }
        
        [FormerlySerializedAs("diceRandomizer")]
        [SerializeField]
        private DieRandomizer dieRandomizer;

        public Die()
        {
            value = 6;
            locked = false;
            dieRandomizer = DieRandomizer.D6;
        }

        public void Roll()
        {
            value = dieRandomizer.Randomize();
        }

        public void ToggleLock()
        {
            locked = !locked;
        }
    }
}
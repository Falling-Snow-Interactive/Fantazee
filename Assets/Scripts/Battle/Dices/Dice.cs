using System;
using ProjectYahtzee.Battle.Dices.Randomizer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectYahtzee.Battle.Dices
{
    [Serializable]
    public class Dice
    {
        [SerializeField]
        private int value = 6;
        public int Value => value;

        [SerializeField]
        private bool locked;

        public bool Locked
        {
            get => locked;
            set => locked = value;
        }
        
        [SerializeField]
        private DiceRandomizer diceRandomizer;

        public Dice()
        {
            value = 6;
            locked = false;
            diceRandomizer = DiceRandomizer.D6;
        }

        public void Roll()
        {
            value = diceRandomizer.Randomize();
        }

        public void ToggleLock()
        {
            locked = !locked;
        }
    }
}
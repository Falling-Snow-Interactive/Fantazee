using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ProjectYahtzee.Gameplay.Dices
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

        public void Roll()
        {
            value = Random.Range(0, 6) + 1;
        }

        public void ToggleLock()
        {
            locked = !locked;
        }
    }
}
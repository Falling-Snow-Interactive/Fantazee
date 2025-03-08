using System;
using System.Collections.Generic;
using Fantazee.Dice.Randomizer;
using Fantazee.Items.Dice.Randomizer;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Dice
{
    [Serializable]
    public class Die : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
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
        public DieRandomizer DieRandomizer => dieRandomizer;

        public Die(int value = 6)
        {
            this.value = value;
            dieRandomizer = DieRandomizer.D6;
        }

        public void Roll()
        {
            value = dieRandomizer.Randomize();
        }

        public override string ToString()
        {
            string s = "";

            if (dieRandomizer == null)
            {
                dieRandomizer = new();
            }
            for (int i = 0; i < dieRandomizer.Entries.Count; i++)
            {
                DieRandomizerEntry d = dieRandomizer.Entries[i];
                s += d.Value + " - " + d.Weight;

                if (i + 1 < dieRandomizer.Entries.Count)
                {
                    s += "     ";
                }
            }

            return s;
        }

        public void OnBeforeSerialize()
        {
            name = ToString();
        }

        public void OnAfterDeserialize() { }
        
        public static List<Die> DefaultDice(int amount)
        {
            List<Die> dice = new();
            for (int i = 0; i < amount; i++)
            {
                dice.Add(new Die(i));
            }
            return dice;
        }
    }
}
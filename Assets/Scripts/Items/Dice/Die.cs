using System;
using ProjectYahtzee.Items.Dice.Randomizer;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Items.Dice
{
    [Serializable]
    public class Die : Item, ISerializationCallbackReceiver
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

        public void OnBeforeSerialize()
        {
            string s = "";

            for (int i = 0; i < dieRandomizer.Entries.Count; i++)
            {
                DieRandomizerEntry d = dieRandomizer.Entries[i];
                s += d.Value + " - " + d.Weight;

                if (i + 1 < dieRandomizer.Entries.Count)
                {
                    s += "     ";
                }
            }

            name = s;
        }

        public void OnAfterDeserialize() { }
    }
}
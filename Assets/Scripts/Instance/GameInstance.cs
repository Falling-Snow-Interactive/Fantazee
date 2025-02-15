using System;
using System.Collections.Generic;
using Fsi.Gameplay.Healths;
using ProjectYahtzee.Boons;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Instance
{
    [Serializable]
    public class GameInstance
    {
        [SerializeField]
        private uint seed = 982186532;
        public uint Seed
        {
            get => seed;
            set => seed = value;
        }

        [Header("Character")]

        [SerializeField]
        private Health health;
        public Health Health => health;
        
        [Header("Dice")]
        
        [SerializeField]
        private List<Battle.Dices.Dice> dice; // TODO - For some reason this is getting cleared when the game exits - KD
        public List<Battle.Dices.Dice> Dice => dice;

        [Header("Maps")]
        
        [FormerlySerializedAs("mapNode")]
        [SerializeField]
        private int currentMapIndex = 0;
        public int CurrentMapIndex
        {
            get => currentMapIndex;
            set => currentMapIndex = value;
        }
        
        [Header("Boons")]
        
        [SerializeReference]
        private List<Boon> boons = new List<Boon>();
        public List<Boon> Boons => boons;

        public void ResetDice()
        {
            dice.Clear();
            for (int i = 0; i < 5; i++)
            {
                dice.Add(new Battle.Dices.Dice());
            }
        }
    }
}

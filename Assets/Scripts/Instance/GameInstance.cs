using System;
using System.Collections.Generic;
using Fantazee.Battle.Environments;
using Fantazee.Battle.Scores;
using Fantazee.Boons;
using Fantazee.Currencies;
using Fantazee.Dice;
using Fsi.Gameplay.Healths;
using Fantazee.Items.Dice;
using Fantazee.Relics;
using Fantazee.Spells;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Instance
{
    [Serializable]
    public class GameInstance
    {
        private const uint DefaultSeed = 0;
        private const int DefaultHealth = 500;
        private const int DefaultGold = 500;
        
        [SerializeField]
        private uint seed;
        public uint Seed => seed;

        [Header("Character")]

        [SerializeField]
        private Health health;
        public Health Health => health;

        [Header("Dice")]
        
        [SerializeField]
        private List<Die> dice; // TODO - For some reason this is getting cleared when the game exits - KD
        public List<Die> Dice => dice;

        [Header("Maps")]

        [SerializeField]
        private int mapIndex = 0;
        public int MapIndex
        {
            get => mapIndex;
            set => mapIndex = value;
        }
        
        [SerializeField]
        private int mapNodeIndex = 0;
        public int MapNodeIndex
        {
            get => mapNodeIndex;
            set => mapNodeIndex = value;
        }
        
        [SerializeField]
        private EnvironmentType environment;
        public EnvironmentType Environment => environment;
        
        [Header("Boons")]
        
        [SerializeReference]
        private List<Boon> boons;
        public List<Boon> Boons => boons;

        [Header("Currency")]

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        [Header("Relics")]

        [SerializeField]
        private List<RelicInstance> relics;
        public List<RelicInstance> Relics => relics;
        
        [Header("Scores")]
        
        [SerializeField]
        private ScoreTracker scoreTracker;
        public ScoreTracker ScoreTracker => scoreTracker;
        
        public static GameInstance Defaults
        {
            get
            {
                GameInstance instance = new()
                                        {
                                            seed = DefaultSeed,
                                            health = new Health(DefaultHealth),
                                            mapIndex = 0,
                                            mapNodeIndex = 0,
                                            dice = DefaultDice(6),
                                            boons = new List<Boon>(),
                                            wallet = new Wallet(CurrencyType.Gold, DefaultGold),
                                            environment = EnvironmentType.Woods,
                                        };
                
                return instance;
            }
        }
        
        public static List<Die> DefaultDice(int amount)
        {
            List<Die> dice = new();
            for (int i = 0; i < amount; i++)
            {
                dice.Add(new Die(i));
            }
            return dice;
        }

        public void ResetDice()
        {
            dice = DefaultDice(6);
        }

        public void RandomizeSeed()
        {
            seed = (uint)Random.Range(0, int.MaxValue);
        }

        public void AdvanceMap()
        {
            Debug.Log($"Instance: Advance map: \nIndex {mapIndex} -> {mapIndex + 1}");
            mapIndex++;
            mapNodeIndex = 0;
        }
    }
}

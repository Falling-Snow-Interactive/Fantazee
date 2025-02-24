using System;
using System.Collections.Generic;
using Fantazee.Battle.Environments;
using Fantazee.Characters;
using Fantazee.Characters.Settings;
using Fantazee.Currencies;
using Fantazee.Relics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Fantazee.Instance
{
    [Serializable]
    public class GameInstance
    {
        public static GameInstance Current => GameController.Instance.GameInstance;
        
        private const uint DefaultSeed = 0;
        private const int DefaultGold = 500;
        
        [SerializeField]
        private uint seed;
        public uint Seed => seed;

        [Header("Character")]

        [SerializeReference]
        private CharacterInstance character;
        public CharacterInstance Character => character;

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

        [Header("Currency")]

        [SerializeField]
        private Wallet wallet;
        public Wallet Wallet => wallet;

        [Header("Relics")]

        [SerializeField]
        private List<RelicInstance> relics;
        public List<RelicInstance> Relics => relics;
        
        public static GameInstance Defaults
        {
            get
            {
                GameInstance instance = new()
                                        {
                                            seed = DefaultSeed,
                                            character = new CharacterInstance(CharacterSettings.Settings.DefaultCharacter),
                                            mapIndex = 0,
                                            mapNodeIndex = 0,
                                            wallet = new Wallet(CurrencyType.Gold, DefaultGold),
                                            environment = EnvironmentType.Woods,
                                        };
                
                return instance;
            }
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

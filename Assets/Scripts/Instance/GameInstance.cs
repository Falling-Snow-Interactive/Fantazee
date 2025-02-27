using System;
using System.Collections.Generic;
using Fantazee.Characters.Settings;
using Fantazee.Currencies;
using Fantazee.Maps;
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

        [SerializeReference]
        private MapInstance map;
        public MapInstance Map => map;

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
                                            map = new MapInstance(),
                                        };
                
                return instance;
            }
        }

        public void RandomizeSeed()
        {
            seed = (uint)Random.Range(0, int.MaxValue);
        }
    }
}

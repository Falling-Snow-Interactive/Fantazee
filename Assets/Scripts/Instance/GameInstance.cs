using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.Characters.Settings;
using Fantazee.Currencies;
using Fantazee.Environments;
using Fantazee.Environments.Settings;
using Fantazee.Maps;
using Fantazee.Relics;
using UnityEngine;
using UnityEngine.Serialization;
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

        [FormerlySerializedAs("map")]
        [Header("Maps")]

        [SerializeReference]
        private EnvironmentInstance environment;
        public EnvironmentInstance Environment => environment;
        
        public static GameInstance Defaults
        {
            get
            {
                GameInstance instance = new()
                                        {
                                            seed = (uint)Random.Range(0, int.MaxValue),
                                            character = new CharacterInstance(CharacterSettings.Settings.DefaultCharacter),
                                            environment = new EnvironmentInstance(EnvironmentSettings.Settings.DefaultEnvironment),
                                        };
                
                return instance;
            }
        }
        
        public GameInstance(){}

        public GameInstance(CharacterData character, EnvironmentData environment)
        {
            RandomizeSeed();
            this.character = new CharacterInstance(character);
            this.environment = new EnvironmentInstance(environment);
        }

        public void RandomizeSeed()
        {
            seed = (uint)Random.Range(0, int.MaxValue);
        }

        public void Clear()
        {
            character.Clear();
        }
    }
}

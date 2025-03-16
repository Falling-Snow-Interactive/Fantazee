using System;
using Fantazee.Instance;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class GameSave
    {
        [SerializeField]
        private uint seed;
        public uint Seed => seed;

        [SerializeField]
        private CharacterSave character;
        public CharacterSave Character => character;
        
        [SerializeField]
        private EnvironmentSave environment;
        public EnvironmentSave Environment => environment;
        
        public GameSave(GameInstance game)
        {
            seed = game.Seed;
            character = new CharacterSave(game.Character);
            environment = new EnvironmentSave(game.Environment);
        }
    }
}

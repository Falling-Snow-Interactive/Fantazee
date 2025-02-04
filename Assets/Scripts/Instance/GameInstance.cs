using System;
using System.Collections.Generic;
using Fsi.Gameplay.Healths;
using ProjectYahtzee.Gameplay.Scores;
using ProjectYahtzee.Maps;
using ProjectYahtzee.Maps.Settings;
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
        
        [FormerlySerializedAs("score")]
        [SerializeField]
        private List<Score> scores = new List<Score>();
        public List<Score> Scores => scores;

        [Header("Character")]

        [SerializeField]
        private Health health;
        public Health Health => health;
        
        [Header("Dice")]
        
        [SerializeField]
        private List<Gameplay.Dices.Dice> dice = new();
        public List<Gameplay.Dices.Dice> Dice => dice;

        // TODO - Make serializable
        [SerializeField]
        private Map map;
        public Map Map => map ??= new Map(MapSettings.Settings.MapProperties, seed);

        public void ResetScore()
        {
            scores.Clear();
            int x = Enum.GetValues(typeof(ScoreType)).Length;
            for (int i = 1; i < x; i++)
            {
                ScoreType type = (ScoreType) i;
                Score score = new(type, 0);
                scores.Add(score);
            }
        }

        public void ResetDice()
        {
            dice.Clear();
            for (int i = 0; i < 5; i++)
            {
                dice.Add(new Gameplay.Dices.Dice());
            }
        }
    }
}

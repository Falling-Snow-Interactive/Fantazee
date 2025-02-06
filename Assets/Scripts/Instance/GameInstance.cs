using System;
using System.Collections.Generic;
using Fsi.Gameplay.Healths;
using ProjectYahtzee.Battle.Scores;
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
        private List<Battle.Dices.Dice> dice; // TODO - For some reason this is getting cleared when the game exits - KD
        public List<Battle.Dices.Dice> Dice => dice;

        [FormerlySerializedAs("mapNode")]
        [SerializeField]
        private int currentMapIndex = 0;
        public int CurrentMapIndex
        {
            get => currentMapIndex;
            set => currentMapIndex = value;
        }

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
                dice.Add(new Battle.Dices.Dice());
            }
        }
    }
}

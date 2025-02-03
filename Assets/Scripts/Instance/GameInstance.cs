using System;
using System.Collections.Generic;
using Fsi.Gameplay.Healths;
using ProjectYahtzee.Gameplay.Scores;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Instance
{
    [Serializable]
    public class GameInstance
    {
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

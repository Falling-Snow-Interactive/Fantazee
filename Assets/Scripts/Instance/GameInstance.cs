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
    }
}

using System;
using System.Collections.Generic;
using Fantazee.Characters;
using UnityEngine;

namespace Fantazee.Scores
{
    [Serializable]
    public class ScoreTracker
    {
        [SerializeReference]
        private List<Score> scores = new List<Score>();
        public List<Score> Scores => scores;

        [SerializeReference]
        private FantazeeScore fantazee;
        public FantazeeScore Fantazee => fantazee;

        // [SerializeField]
        // private BonusScore bonusScore;
        // public BonusScore BonusScore => bonusScore;

        public ScoreTracker(CharacterData data)
        {
            foreach (PlayerScoreData sd in data.ScoreData)
            {
                Score score = ScoreFactory.Create(sd);
                scores.Add(score);
            }

            fantazee = new FantazeeScore(data.FantazeeSpells);
        }
    }
}
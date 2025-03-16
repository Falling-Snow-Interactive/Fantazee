using System;
using System.Collections.Generic;
using Fantazee.Characters;
using Fantazee.SaveLoad;
using Fantazee.Scores.Instance;
using UnityEngine;

namespace Fantazee.Scores.Scoresheets
{
    [Serializable]
    public class Scoresheet
    {
        [SerializeReference]
        private List<ScoreInstance> scores = new();
        public List<ScoreInstance> Scores => scores;

        [SerializeField]
        private FantazeeScoreInstance fantazee;
        public FantazeeScoreInstance Fantazee => fantazee;

        public Scoresheet(List<CharacterScoreData> scoreData, CharacterScoreData fantazeeData)
        {
            foreach (CharacterScoreData sd in scoreData)
            {
                ScoreInstance score = ScoreFactory.CreateInstance(sd);
                scores.Add(score);
            }
            
            fantazee = new FantazeeScoreInstance(fantazeeData);
        }

        public Scoresheet(ScoresheetSave save)
        {
            foreach (ScoreSave scoreSave in save.Scores)
            {
                ScoreInstance score = ScoreFactory.CreateInstance(scoreSave);
                scores.Add(score);
            }

            fantazee = new FantazeeScoreInstance(save.FantazeeScore);
        }
    }
}
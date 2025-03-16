using System;
using System.Collections.Generic;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Scoresheets;
using UnityEngine;

namespace Fantazee.SaveLoad
{
    [Serializable]
    public class ScoresheetSave
    {
        [SerializeField]
        private List<ScoreSave> scores;
        public List<ScoreSave> Scores => scores;

        [SerializeField]
        private ScoreSave fantazeeScore;
        public ScoreSave FantazeeScore => fantazeeScore;
        
        public ScoresheetSave(Scoresheet scoresheet)
        {
            scores = new List<ScoreSave>();
            foreach (ScoreInstance score in scoresheet.Scores)
            {
                ScoreSave ss = new(score);
                scores.Add(ss);
            }

            fantazeeScore = new ScoreSave(scoresheet.Fantazee);
        }
    }
}
using System;
using System.Collections.Generic;
using ProjectYahtzee.Battle.Scores;
using ProjectYahtzee.Battle.Scores.Ui;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Boons;
using UnityEngine;

namespace ProjectYahtzee.Battle.Scores
{
    [Serializable]
    public class ScoreTracker
    {
        private Dictionary<ScoreType, int> scores = new Dictionary<ScoreType, int>();

        public void Initialize()
        {
            scores.Clear();
        }

        public void AddScore(Score score, int value)
        {
            scores.Add(score.Type, value);
        }
        
        public int GetTotal()
        {
            int total = 0;
            foreach (KeyValuePair<ScoreType, int> score in scores)
            {
                total += score.Value;
            }

            return total;
        }

        public bool CanScore(ScoreType type)
        {
            return !scores.ContainsKey(type);
        }

        public int GetScore(ScoreType type)
        {
            return scores.GetValueOrDefault(type, 0);
        }
    }
}
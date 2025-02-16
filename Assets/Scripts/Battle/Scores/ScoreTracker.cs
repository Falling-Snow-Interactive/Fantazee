using System;
using System.Collections.Generic;
using ProjectYahtzee.Battle.Scores;
using ProjectYahtzee.Battle.Scores.Bonus;
using ProjectYahtzee.Battle.Scores.Ui;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Boons;
using UnityEngine;

namespace ProjectYahtzee.Battle.Scores
{
    [Serializable]
    public class ScoreTracker
    {
        private Dictionary<ScoreType, int> scoreDictionary = new Dictionary<ScoreType, int>();

        public List<Score> Scores { get; } = new();

        private BonusScore bonusScore;
        public BonusScore BonusScore => bonusScore;

        public void Initialize()
        {
            scoreDictionary.Clear();
            
            Scores.Clear();
            int x = Enum.GetValues(typeof(ScoreType)).Length;
            for (int i = 1; i < x; i++)
            {
                ScoreType type = (ScoreType) i;
                Score score = ScoreFactory.Create(type);
                Scores.Add(score);
            }
            bonusScore = new BonusScore();
            
            BattleUi.Instance.Scoreboard.Initialize();
        }

        public void AddScore(Score score, int value)
        {
            scoreDictionary.Add(score.Type, value);
            bonusScore.Add(value);
        }
        
        public int GetTotal()
        {
            int total = 0;
            foreach (KeyValuePair<ScoreType, int> score in scoreDictionary)
            {
                total += score.Value;
            }

            return total;
        }

        public bool CanScore(ScoreType type)
        {
            return !scoreDictionary.ContainsKey(type);
        }

        public int GetScore(ScoreType type)
        {
            return scoreDictionary.GetValueOrDefault(type, 0);
        }
    }
}
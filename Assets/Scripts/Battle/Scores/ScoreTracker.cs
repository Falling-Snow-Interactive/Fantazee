using System;
using System.Collections.Generic;
using Fantazhee.Battle.Scores;
using Fantazhee.Battle.Scores.Ui;
using Fantazhee.Boons;
using Fantazhee.Battle.Scores.Bonus;
using Fantazhee.Battle.Ui;
using UnityEngine;

namespace Fantazhee.Battle.Scores
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
            Debug.Log($"Score Tracker - Initialize");
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
            Debug.Log($"Score Tracker - Add {score.Type} - {value}");
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
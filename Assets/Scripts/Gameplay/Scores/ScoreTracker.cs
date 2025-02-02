using System;
using System.Collections.Generic;
using ProjectYahtzee.Gameplay.Scores;

namespace ProjectYahtzee.Gameplay.Scores
{
    [Serializable]
    public class ScoreTracker
    {
        private Dictionary<ScoreType, int> scores = new Dictionary<ScoreType, int>();

        public void Initialize()
        {
            scores.Clear();
        }

        public void AddScore(Score score, List<Dices.Dice> dice)
        {
            scores.Add(score.Type, ScoreCalculator.Calculate(score, dice));
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
            return scores[type];
        }
    }
}
using System;
using System.Collections.Generic;
using ProjectYahtzee.Battle.Scores;

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

        public int AddScore(Score score, List<Dices.Dice> dice)
        {
            int calculate = ScoreCalculator.Calculate(score, dice);
            scores.Add(score.Type, calculate);
            return calculate;
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
using System;
using System.Collections.Generic;
using ProjectYahtzee.Battle.Scores;
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

        public int AddScore(Score score, List<Dices.Dice> dice)
        {
            int diceScore = ScoreCalculator.Calculate(score.Type, dice);
            float value = score.Value;
            float mod = score.Mod;

            foreach (Boon boon in GameController.Instance.GameInstance.Boons)
            {
                float v = boon.GetValue();
                float m = boon.GetModifier();

                if (v > 0)
                {
                    Debug.Log($"Adding value to boon: {v}");
                }
                
                if (m > 0)
                {
                    Debug.Log($"Adding modifier to boon: {m}");
                }

                value += v;
                mod += m;
            }

            float total = (diceScore + value) * mod;
            int rounded = Mathf.RoundToInt(total);

            scores.Add(score.Type, rounded);
            return rounded;
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
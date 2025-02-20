using System;
using Fantazee.Battle.Settings;

namespace Fantazee.Battle.Scores.Bonus
{
    public class BonusScore
    {
        public event Action Changed;

        public int Max => GameplaySettings.Settings.BonusScore;
        public int CurrentScore { get; private set; }
        public float Normalized => (float)CurrentScore / Max;
        
        public bool IsReady => CurrentScore >= Max;
        
        public BonusScore()
        {
            CurrentScore = 0;
        }

        public void Add(int score)
        {
            CurrentScore += score;
            Changed?.Invoke();
        }

        public void Reset()
        {
            CurrentScore = 0;
            Changed?.Invoke();
        }
    }
}

using Fantazee.Scores;
using Fantazee.Scores.Instance;

namespace Fantazee.Battle.Score
{
    public class FantazeeBattleScore : BattleScore
    {
        
        // TODO - Check parent class
        // public FantazeeScore FantazeeScore { get; }
        public FantazeeBattleScore(ScoreInstance score) : base(score)
        {
            // FantazeeScore = score;
        }
    }
}

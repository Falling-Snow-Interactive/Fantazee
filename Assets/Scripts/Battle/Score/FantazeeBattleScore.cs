using Fantazee.Scores;

namespace Fantazee.Battle.Score
{
    public class FantazeeBattleScore : BattleScore
    {
        public FantazeeScore FantazeeScore { get; }
        public FantazeeBattleScore(FantazeeScore score) : base(score)
        {
            FantazeeScore = score;
        }
    }
}

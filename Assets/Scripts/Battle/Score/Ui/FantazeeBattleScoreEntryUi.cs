using System;
using Fantazee.Scores;
using Fantazee.Scores.Ui.ScoreEntries;

namespace Fantazee.Battle.Score.Ui
{
    public class FantazeeBattleScoreEntryUi : BattleScoreEntry
    {
        public void Initialize(BattleScore battleScore, Action<ScoreEntry> onSelect)
        {
            base.Initialize(battleScore.Score, onSelect);

            if (battleScore.Score is FantazeeScore fantazeeScore)
            {
                for (int i = 0; i < Spells.Count; i++)
                {
                    Spells[i].Initialize(i, fantazeeScore.Spells[i]);
                }
            }
        }
    }
}

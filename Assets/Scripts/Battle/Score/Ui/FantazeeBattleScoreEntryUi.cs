using System;
using Fantazee.Scores.Ui.ScoreEntries;

namespace Fantazee.Battle.Score.Ui
{
    public class FantazeeBattleScoreEntryUi : BattleScoreEntry
    {
        public new void Initialize(BattleScore battleScore, Action<ScoreEntry> onSelect)
        {
            base.Initialize(battleScore.Score, onSelect);
            for (int i = 0; i < Spells.Count; i++)
            {
                Spells[i].Initialize(i, battleScore.Score.Spells[i]);
            }
        }
    }
}

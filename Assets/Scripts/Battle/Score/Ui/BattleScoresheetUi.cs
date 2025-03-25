using System;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoresheetUi : MonoBehaviour
    {
        private Action<BattleScoreButton> onScoreSelected;
        
        [SerializeField]
        private List<BattleScoreButton> scoreButtons = new();
        public List<BattleScoreButton> ScoreButtons => scoreButtons;

        [SerializeField]
        private BattleScoreButton fantazeeBattleButton;
        
        public void Initialize(List<BattleScore> scores, 
                               BattleScore fantazeeScore, 
                               Action<BattleScoreButton> onScoreSelected)
        {
            this.onScoreSelected = onScoreSelected;
            
            Debug.Assert(scores.Count == scoreButtons.Count);
            for (int i = 0; i < scores.Count; i++)
            {
                BattleScore battleScore = scores[i];
                BattleScoreButton battleButton = scoreButtons[i];
                battleButton.Initialize(battleScore, OnScoreEntrySelected);
            }
            
            fantazeeBattleButton.Initialize(fantazeeScore, OnScoreEntrySelected);
        }
        
        private void OnScoreEntrySelected(BattleScoreButton battleButton)
        {
            onScoreSelected?.Invoke(battleButton);
        }
    }
}
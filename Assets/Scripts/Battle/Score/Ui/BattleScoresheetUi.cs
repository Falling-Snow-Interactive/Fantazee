using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoresheetUi : ScoresheetUi
    {
        [SerializeField]
        private List<BattleScoreButton> battleScoreButtons = new();

        [SerializeField]
        private BattleScoreButton fantazeeBattleButton;
        
        public void Initialize(List<BattleScore> scores, 
                               BattleScore fantazeeScore, 
                               Action<BattleScoreButton> onButtonSelected)
        {
            Debug.Assert(scores.Count == battleScoreButtons.Count);
            for (int i = 0; i < scores.Count; i++)
            {
                BattleScore battleScore = scores[i];
                BattleScoreButton battleButton = battleScoreButtons[i];
                battleButton.Initialize(battleScore, onButtonSelected);
            }
            
            fantazeeBattleButton.Initialize(fantazeeScore, onButtonSelected);
        }
    }
}
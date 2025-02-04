using Fsi.Gameplay;
using ProjectYahtzee.Battle.Scores.Ui;
using ProjectYahtzee.Battle.Ui.Dices.DiceControl;
using UnityEngine;

namespace ProjectYahtzee.Battle.Ui
{
    public class GameplayUi : MbSingleton<GameplayUi>
    {
        [SerializeField]
        private DiceControlUi diceControl;
        public DiceControlUi DiceControl => diceControl;
        
        [SerializeField]
        private ScoreboardUi scoreboard;
        public ScoreboardUi Scoreboard => scoreboard;
    }
}
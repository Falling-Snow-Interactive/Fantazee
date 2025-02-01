using Fsi.Gameplay;
using ProjectYahtzee.Gameplay.Scores.Ui;
using ProjectYahtzee.Gameplay.Ui.Dices.DiceControl;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui
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
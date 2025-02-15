using Fsi.Gameplay;
using ProjectYahtzee.Battle.Boons.Ui;
using ProjectYahtzee.Battle.Dices.Ui.DiceControl;
using ProjectYahtzee.Battle.Scores.Ui;
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

        [SerializeField]
        private BoonsUi boonsUi;
        public BoonsUi BoonsUi => boonsUi;
    }
}
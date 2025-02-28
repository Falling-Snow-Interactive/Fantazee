using Fantazee.Battle.Score.Ui;
using Fantazee.Battle.Ui.WinScreens;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazee.Battle.Ui
{
    public class BattleUi : MbSingleton<BattleUi>
    {
        [SerializeField]
        private DiceControlUi diceControl;
        public DiceControlUi DiceControl => diceControl;
        
        [SerializeField]
        private ScoreboardUi scoreboard;
        public ScoreboardUi Scoreboard => scoreboard;

        [SerializeField]
        private WinScreen winScreen;
        public WinScreen WinScreen => winScreen;

        protected override void Awake()
        {
            base.Awake();

            diceControl.gameObject.SetActive(true);
            scoreboard.gameObject.SetActive(true);
            
            winScreen.gameObject.SetActive(false);
        }

        public void ShowWinScreen()
        {
            winScreen.gameObject.SetActive(true);
        }
    }
}
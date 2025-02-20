using Fantazhee.Battle.Boons.Ui;
using Fantazhee.Battle.Scores.Ui;
using Fantazhee.Battle.Ui.WinScreens;
using Fsi.Gameplay;
using UnityEngine;

namespace Fantazhee.Battle.Ui
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
        private BoonsUi boonsUi;
        public BoonsUi BoonsUi => boonsUi;

        [SerializeField]
        private WinScreen winScreen;
        public WinScreen WinScreen => winScreen;

        protected override void Awake()
        {
            base.Awake();

            diceControl.gameObject.SetActive(true);
            scoreboard.gameObject.SetActive(true);
            boonsUi.gameObject.SetActive(true);
            
            winScreen.gameObject.SetActive(false);
        }

        public void ShowWinScreen()
        {
            winScreen.gameObject.SetActive(true);
        }
    }
}
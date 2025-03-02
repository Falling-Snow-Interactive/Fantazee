using System;
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

        [SerializeField]
        private GameObject helpScreen;
        public GameObject HelpScreen => helpScreen;
        
        private FsiInput input;

        protected override void Awake()
        {
            base.Awake();

            diceControl.gameObject.SetActive(true);
            scoreboard.gameObject.SetActive(true);
            
            winScreen.gameObject.SetActive(false);
            
            input = new FsiInput();
            
            input.Gameplay.Help.performed += ctx => helpScreen.gameObject.SetActive(!helpScreen.gameObject.activeInHierarchy);
        }

        private void OnEnable()
        {
            input.Gameplay.Enable();
        }

        private void OnDisable()
        {
            input.Gameplay.Disable();
        }

        public void ShowWinScreen()
        {
            winScreen.gameObject.SetActive(true);
        }
    }
}
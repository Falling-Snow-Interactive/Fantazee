using System;
using Fantazee.Battle.Relics.Ui;
using Fantazee.Battle.Score.Ui;
using Fantazee.Battle.Ui.WinScreens;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Ui
{
    public class BattleUi : MbSingleton<BattleUi>
    {
        [SerializeField]
        private DiceControlUi diceControl;
        public DiceControlUi DiceControl => diceControl;
        
        [FormerlySerializedAs("scoreboard")]
        [SerializeField]
        private ScoresheetUi scoresheet;
        public ScoresheetUi Scoresheet => scoresheet;

        [SerializeField]
        private WinScreen winScreen;
        public WinScreen WinScreen => winScreen;

        [SerializeField]
        private GameObject helpScreen;
        public GameObject HelpScreen => helpScreen;
        
        [SerializeField]
        private RelicUi relicUi;
        public RelicUi RelicUi => relicUi;
        
        private FsiInput input;

        protected override void Awake()
        {
            base.Awake();

            diceControl.gameObject.SetActive(true);
            scoresheet.gameObject.SetActive(true);
            
            winScreen.gameObject.SetActive(false);
            
            helpScreen.gameObject.SetActive(false);
            
            input = new FsiInput();
            
            input.Gameplay.Help.started += ctx => helpScreen.gameObject.SetActive(true);
            input.Gameplay.Help.canceled += ctx => helpScreen.gameObject.SetActive(false);
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
using Fantazee.Battle.Relics.Ui;
using Fantazee.Battle.Score.Ui;
using Fantazee.Battle.Ui.WinScreens;
using Fantazee.Instance;
using Fsi.Gameplay;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class BattleUi : MbSingleton<BattleUi>
    {
        [SerializeField]
        private DiceControlUi diceControl;
        public DiceControlUi DiceControl => diceControl;
        
        [SerializeField]
        private BattleScoresheetUi scoresheet;
        public BattleScoresheetUi Scoresheet => scoresheet;

        [SerializeField]
        private WinScreen winScreen;
        public WinScreen WinScreen => winScreen;

        [SerializeField]
        private GameObject helpScreen;
        public GameObject HelpScreen => helpScreen;
        
        [SerializeField]
        private RelicUi relicUi;
        public RelicUi RelicUi => relicUi;
        
        [SerializeField]
        private Image backgroundImage;

        [Header("Input")]

        [SerializeField]
        private InputActionReference helpActionReference;
        private InputAction helpAction;
        
        // private FsiInput input;
        
        protected override void Awake()
        {
            base.Awake();

            diceControl.gameObject.SetActive(true);
            scoresheet.gameObject.SetActive(true);
            
            winScreen.gameObject.SetActive(false);
            
            helpScreen.gameObject.SetActive(false);

            helpAction = helpActionReference.ToInputAction();
        }

        private void OnEnable()
        {
            helpAction.started += OnHelpActionStarted;
            helpAction.canceled += OnHelpActionCanceled;
            
            helpAction.Enable();
        }

        private void OnDisable()
        {
            helpAction.started -= OnHelpActionStarted;
            helpAction.canceled -= OnHelpActionCanceled;
            
            helpAction.Disable();
        }

        private void Start()
        {
            if (backgroundImage)
            {
                backgroundImage.color = GameInstance.Current.Environment.Data.Color;
            }

            Debug.Log($"BattleUi: Selecting Game Object ({scoresheet.ScoreButtons[0].gameObject.name}).");
            EventSystem.current.SetSelectedGameObject(scoresheet.ScoreButtons[0].gameObject);
        }

        public void ShowWinScreen()
        {
            winScreen.gameObject.SetActive(true);
        }
        
        private void OnHelpActionCanceled(InputAction.CallbackContext ctx)
        {
            helpScreen.gameObject.SetActive(false);
        }

        private void OnHelpActionStarted(InputAction.CallbackContext ctx)
        {
            helpScreen.gameObject.SetActive(true);
        }
    }
}
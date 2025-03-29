using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Scores;
using Fantazee.Ui;
using Fantazee.Ui.Buttons;
using UnityEngine.InputSystem;

namespace Fantazee.Battle.Ui
{
    public class EndTurnButton : SimpleButton
    {
        private BattlePlayer player;

        [Header("Color Overrides")]

        [SerializeField]
        private BackgroundColorPalette colorPalette = BackgroundColorPalette.Default;
        protected override BackgroundColorPalette ColorPalette => colorPalette;

        [Header("     Input")]

        [SerializeField]
        private InputActionReference endActionRef;
        private InputAction endAction;

        private void Awake()
        {
            endAction = endActionRef.ToInputAction();

            endAction.performed += ctx => OnClick();
        }
        
        private void OnEnable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.Scored += OnScored;
                player.TurnEnded += OnTurnEnd;
            }
            
            endAction.Enable();
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned -= OnCharacterSpawned;

            if (player)
            {
                player.Scored -= OnScored;
                player.TurnEnded -= OnTurnEnd;
            }
            
            endAction.Disable();
        }

        private void Start()
        {
            IsDisabled = true;
            button.interactable = false;
            UpdateColors();
        }

        private void OnCharacterSpawned(BattleCharacter character)
        {
            if (character is BattlePlayer player)
            {
                this.player = player;
                
                player.Scored += OnScored;
                player.TurnEnded += OnTurnEnd;
            }
        }

        public override void OnClick()
        {
            base.OnClick();
            if (!IsDisabled)
            {
                BattleController.Instance.Player.EndTurn();
            }
        }

        private void OnScored(ScoreResults scoreResults)
        {
            button.interactable = true;
            IsDisabled = false;
            UpdateColors();
        }

        private void OnTurnEnd()
        {
            button.interactable = false;
            IsDisabled = true;
            UpdateColors();
        }
    }
}
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Fantazee.Battle.Ui
{
    public class RollButton : SimpleButton, IDeselectHandler
    {
        private BattlePlayer player;

        [Header("     Input")]

        [SerializeField]
        private InputActionReference rollActionRef;
        private InputAction rollAction;
        
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        private void Awake()
        {
            rollAction = rollActionRef.ToInputAction();
        }

        private void OnEnable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.RollsChanged += OnRollsChanged;
            }
            
            rollAction.performed += OnClick;
            rollAction.Enable();
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.RollsChanged += OnRollsChanged;
            }
            
            rollAction.performed -= OnClick;
            rollAction.Disable();
        }

        private void OnCharacterSpawned(BattleCharacter character)
        {
            if (character is BattlePlayer player)
            {
                this.player = player;

                player.RollsChanged += OnRollsChanged;
            }
        }

        private void Start()
        {
            rollsText.text = "";
        }
        
        public override void OnClick()
        {
            base.OnClick();
            BattleController.Instance.Player.TryRoll();
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
        }
        
        private void OnClick(InputAction.CallbackContext ctx)
        {
            OnClick();
        }
        
        private void OnRollsChanged()
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
        }
    }
}
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Scores;
using Fantazee.Ui.Buttons;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Fantazee.Battle.Ui
{
    public class EndTurnButton : SimpleButton, IDeselectHandler, IPointerEnterHandler
    {
        private BattlePlayer player;

        [Header("Ui")]

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
            BattleController.Instance.Player.EndTurn();
        }

        private void OnScored(ScoreResults scoreResults)
        {
            button.interactable = true;
        }

        private void OnTurnEnd()
        {
            button.interactable = false;
        }
    }
}
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Ui.Buttons;
using TMPro;
using UnityEngine.EventSystems;

namespace Fantazee.Battle.Ui
{
    public class RollButton : SimpleButton, ISelectHandler, IDeselectHandler, IPointerEnterHandler
    {
        private BattlePlayer player;
        
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        private void OnEnable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.RollsChanged += OnRollsChanged;
            }
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.RollsChanged += OnRollsChanged;
            }
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
        
        public void OnClick()
        {
            BattleController.Instance.Player.TryRoll();
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
        }
        
        private void OnRollsChanged()
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
        }
    }
}
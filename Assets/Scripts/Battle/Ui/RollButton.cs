using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class RollButton : MonoBehaviour
    {
        private BattlePlayer player;
        
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.TurnStarted += OnTurnStarted;
                player.RollStarted += OnRollStarted;
            }
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.TurnStarted -= OnTurnStarted;
                player.RollStarted -= OnRollStarted;
            }
        }

        private void OnCharacterSpawned(BattleCharacter character)
        {
            if (character is BattlePlayer player)
            {
                this.player = player;

                player.TurnStarted += OnTurnStarted;
                player.RollStarted += OnRollStarted;
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
        
        private void OnTurnStarted()
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
            button.interactable = true;
        }

        private void OnRollStarted()
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
            button.interactable = BattleController.Instance.Player.RollsRemaining > 0;
        }
    }
}
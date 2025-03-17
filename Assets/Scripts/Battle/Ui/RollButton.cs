using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class RollButton : MonoBehaviour, ITurnStartCallbackReceiver, IRollStartedCallbackReceiver
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
                player.RegisterTurnStartReceiver(this);
                player.RegisterRollStartedReceiver(this);
            }
        }
        
        private void OnDisable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.UnregisterTurnStartReceiver(this);
                player.UnregisterRollStartedReceiver(this);
            }
        }

        private void OnCharacterSpawned(BattleCharacter character)
        {
            if (character is BattlePlayer player)
            {
                this.player = player;

                player.RegisterTurnStartReceiver(this);
                player.RegisterRollStartedReceiver(this);
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
        
        public void OnTurnStart(Action onComplete)
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
            button.interactable = true;
        }

        public void OnRollStarted(Action onComplete)
        {
            rollsText.text = BattleController.Instance.Player.RollsRemaining.ToString();
            button.interactable = BattleController.Instance.Player.RollsRemaining > 0;
        }
    }
}
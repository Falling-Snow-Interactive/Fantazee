using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Scores;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class EndTurnButton : MonoBehaviour
    {
        private BattlePlayer player;

        [Header("References")]

        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            BattlePlayer.Spawned += OnCharacterSpawned;

            if (player)
            {
                player.Scored += OnScored;
                player.TurnEnded += OnTurnEnd;
            }
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned -= OnCharacterSpawned;

            if (player)
            {
                player.Scored -= OnScored;
                player.TurnEnded -= OnTurnEnd;
            }
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

        public void OnClick()
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
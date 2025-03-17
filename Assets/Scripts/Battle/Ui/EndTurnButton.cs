using System;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters;
using Fantazee.Battle.Characters.Player;
using Fantazee.Scores;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class EndTurnButton : MonoBehaviour, IScoreCallbackReceiver, ITurnEndCallbackReceiver
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
                player.RegisterScoreReceiver(this);
            }
        }

        private void OnDisable()
        {
            BattlePlayer.Spawned -= OnCharacterSpawned;

            if (player)
            {
                player.UnregisterScoreReceiver(this);
            }
        }

        private void OnCharacterSpawned(BattleCharacter character)
        {
            if (character is BattlePlayer player)
            {
                this.player = player;
                
                player.RegisterScoreReceiver(this);
            }
        }

        public void OnClick()
        {
            BattleController.Instance.Player.EndTurn();
        }

        public void OnScore(ref ScoreResults scoreResults, Action onComplete)
        {
            button.interactable = true;
            onComplete?.Invoke();
        }

        public void OnTurnEnd(Action onComplete)
        {
            button.interactable = false;
        }
    }
}
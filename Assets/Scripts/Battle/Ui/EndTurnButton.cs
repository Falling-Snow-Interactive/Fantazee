using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Battle.Ui
{
    public class EndTurnButton : MonoBehaviour
    {
        [SerializeField] 
        private Button button;
        
        private void OnEnable()
        {
            BattleController.PlayerTurnStart += OnPlayerTurnStart;
            BattleController.Scored += OnScored;
            BattleController.PlayerTurnEnd += OnPlayerTurnEnd;
        }

        private void OnDisable()
        {
            BattleController.PlayerTurnStart -= OnPlayerTurnStart;
            BattleController.Scored -= OnScored;
            BattleController.PlayerTurnEnd -= OnPlayerTurnEnd;
        }

        public void OnClick()
        {
            BattleController.Instance.TryEndPlayerTurn();
        }
        
        private void OnPlayerTurnStart()
        {
            button.interactable = false;
        }
        
        private void OnPlayerTurnEnd()
        {
            button.interactable = false;
        }
        
        private void OnScored(int obj)
        {
            button.interactable = true;
        }
    }
}
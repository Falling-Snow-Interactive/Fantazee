using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.Battle.Ui
{
    public class RollButton : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            BattleController.PlayerTurnStart += OnPlayerTurnStart;
            BattleController.Rolled += OnRolled;
        }

        private void OnDisable()
        {
            BattleController.PlayerTurnStart -= OnPlayerTurnStart;
            BattleController.Rolled -= OnRolled;
        }

        private void Start()
        {
            rollsText.text = BattleController.Instance.RemainingRolls.ToString();
        }
        
        public void OnClick()
        {
            BattleController.Instance.TryRoll();
            rollsText.text = BattleController.Instance.RemainingRolls.ToString();
        }
        
        private void OnRolled()
        {
            rollsText.text = BattleController.Instance.RemainingRolls.ToString();
            if (BattleController.Instance.RemainingRolls == 0)
            {
                button.interactable = false;
            }
        }
        
        private void OnPlayerTurnStart()
        {
            button.interactable = true;
        }
    }
}
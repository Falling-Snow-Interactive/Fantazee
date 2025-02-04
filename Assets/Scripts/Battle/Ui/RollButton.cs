using TMPro;
using UnityEngine;

namespace ProjectYahtzee.Battle.Ui
{
    public class RollButton : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        private void OnEnable()
        {
            BattleController.Rolled += OnRolled;
        }

        private void OnDisable()
        {
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
        }
    }
}
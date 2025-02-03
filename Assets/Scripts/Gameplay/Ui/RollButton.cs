using TMPro;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui
{
    public class RollButton : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private TMP_Text rollsText;

        private void OnEnable()
        {
            GameplayController.Rolled += OnRolled;
        }

        private void OnDisable()
        {
            GameplayController.Rolled -= OnRolled;
        }

        private void Start()
        {
            rollsText.text = GameplayController.Instance.RemainingRolls.ToString();
        }
        
        public void OnClick()
        {
            GameplayController.Instance.TryRoll();
            rollsText.text = GameplayController.Instance.RemainingRolls.ToString();
        }
        
        private void OnRolled()
        {
            rollsText.text = GameplayController.Instance.RemainingRolls.ToString();
        }
    }
}
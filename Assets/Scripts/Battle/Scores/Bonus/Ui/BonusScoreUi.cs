using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazhee.Battle.Scores.Bonus.Ui
{
    public class BonusScoreUi : MonoBehaviour
    {
        private BonusScore bonusScore;
        
        [Header("References")]

        [SerializeField]
        private GameObject readyText;

        [SerializeField]
        private Slider slider;

        [SerializeField]
        private TMP_Text currentScoreText;

        [SerializeField]
        private TMP_Text bonusScoreText;

        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            if (bonusScore != null)
            {
                bonusScore.Changed += UpdateScore;
            }
        }

        private void OnDisable()
        {
            if (bonusScore != null)
            {
                bonusScore.Changed -= UpdateScore;
            }
        }

        public void Initialize(BonusScore bonusScore)
        {
            this.bonusScore = bonusScore;
            
            UpdateScore();

            bonusScore.Changed += UpdateScore;
        }

        private void UpdateScore()
        {
            currentScoreText.text = $"{bonusScore.CurrentScore}";
            bonusScoreText.text = $"{bonusScore.Max}";
            slider.value = bonusScore.Normalized;
            
            readyText.SetActive(bonusScore.IsReady);
        }

        public void OnClick()
        {
            BattleController.Instance.TryBonusAttack();
        }

        public void Enable()
        {
            button.interactable = true;
        }

        public void Disable()
        {
            button.interactable = false;
        }
    }
}

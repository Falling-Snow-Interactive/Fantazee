using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle;
using Fantazee.Battle.Settings;
using Fantazee.Items.Dice.Information;
using Fantazee.Items.Dice.Settings;
using Fantazee.Scores.Information;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui
{
    public class ScoreEntry : MonoBehaviour
    {
        [SerializeReference]
        private Score score;
        public Score Score => score;
        
        [Header("References")]
        
        [SerializeField]
        private TMP_Text nameText;
        
        [FormerlySerializedAs("valueText")]
        [SerializeField]
        private TMP_Text scoreText;
        
        [FormerlySerializedAs("valueContainer")]
        [SerializeField]
        private Transform scoreContainer;

        [SerializeField]
        private Button button;

        [SerializeField]
        private List<Image> diceImages = new();
        public List<Image> DiceImages => diceImages;
        
        private ScoreInformation information;

        private void OnEnable()
        {
            if (score != null)
            {
                score.DieAdded += OnDieAdded;
            }
        }

        private void OnDisable()
        {
            if (score != null)
            {
                score.DieAdded -= OnDieAdded;
            }
        }

        public void Initialize(Score score)
        {
            this.score = score;

            scoreText.text = "";
            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score.Type, out information))
            {
                nameText.text = information.LocName.GetLocalizedString();
                ShowDice(new List<int> {0, 0, 0, 0, 0 });
            }
            
            score.Changed += OnDieAdded;
        }

        private void ShowDice(List<int> values)
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                diceImages[i].gameObject.SetActive(values.Count > i);
                if (i < values.Count 
                    && DiceSettings.Settings.SideInformation.TryGetInformation(values[i], 
                                                                               out SideInformation information))
                {
                    diceImages[i].sprite = information.Sprite;
                }
            }
        }

        private void ShowDice(int[] values)
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                diceImages[i].gameObject.SetActive(values.Length > i);
                if (i < values.Length 
                    && DiceSettings.Settings.SideInformation.TryGetInformation(values[i], 
                                                                               out SideInformation information))
                {
                    diceImages[i].sprite = information.Sprite;
                }
            }

            BattleSettings settings = BattleSettings.Settings;
            diceImages[score.Dice.Count - 1].transform.DOPunchScale(settings.SquishAmount, 
                                                                    settings.SquishTime, 
                                                                    10, 
                                                                    1f)
                                            .SetEase(settings.SquishEase);
        }

        public void FinalizeScore()
        {
            button.interactable = false;
            scoreText.text = score.Calculate().ToString();
            
            scoreContainer.transform.DOPunchScale(BattleSettings.Settings.SquishAmount, 
                                                  BattleSettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(BattleSettings.Settings.SquishEase);
        }
        
        private void OnDieAdded()
        {
            int[] vals = { 0, 0, 0, 0, 0 };

            for (int i = 0; i < score.Dice.Count; i++)
            {
                vals[i] = score.Dice[i].Value;
            }

            ShowDice(vals);
        }

        public void OnClick()
        {
            BattleController.Instance.SelectScoreEntry(this);
        }
    }
}

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
                for (int i = 0; i < diceImages.Count; i++)
                {
                    ShowInSlot(i, 0);
                }
            }
            
            score.DieAdded += OnDieAdded;
        }

        private void ShowInSlot(int index, int value)
        {
            if (diceImages.Count >= index &&
                DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation info))
            {
                diceImages[index].sprite = info.Sprite;
                diceImages[index].transform.parent.DOPunchScale(Vector3.one * 0.1f, 0.2f, 2, 0.5f);
            }
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
            for (int i = 0; i < score.Dice.Count; i++)
            {
                ShowInSlot(i, score.Dice[i].Value);
            }
        }

        public void OnClick()
        {
            BattleController.Instance.SelectScoreEntry(this);
        }
    }
}

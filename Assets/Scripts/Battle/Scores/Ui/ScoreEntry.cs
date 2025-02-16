using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using ProjectYahtzee.Battle.Dice.Information;
using ProjectYahtzee.Battle.Dice.Settings;
using ProjectYahtzee.Battle.Scores.Information;
using ProjectYahtzee.Battle.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ProjectYahtzee.Battle.Scores.Ui
{
    public class ScoreEntry : MonoBehaviour
    {
        [SerializeField]
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

        public void Initialize(Score score)
        {
            this.score = score;

            scoreText.text = "";
            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(score.Type, out information))
            {
                nameText.text = information.LocName.GetLocalizedString();
                SetDice(new List<int> {0, 0, 0, 0, 0 });
            }
        }

        private void SetDice(List<int> values)
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                diceImages[i].gameObject.SetActive(values.Count > i);
                if (i < values.Count 
                    && DiceSettings.Settings.SideInformation.TryGetInformation(values[i], out var information))
                {
                    diceImages[i].sprite = information.Sprite;
                }
            }
        }

        public void SetDice(List<Dice.Die> diceList)
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                diceImages[i].gameObject.SetActive(diceList.Count > i);
                if (i < diceList.Count 
                    && DiceSettings.Settings.SideInformation.TryGetInformation(diceList[i].Value, 
                                                                               out SideInformation information))
                {
                    diceImages[i].sprite = information.Sprite;
                }
            }
        }

        public void SetDice(int index, int value, bool inScore = true)
        {
            diceImages[index].gameObject.SetActive(true);
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation information))
            {
                diceImages[index].sprite = information.Sprite;
            }
            
            Color color = inScore ? Color.white : Color.gray;
            diceImages[index].color = color;
        }

        public void SetScore(int value)
        {
            button.interactable = false;
            scoreText.text = value.ToString();
            
            scoreContainer.transform.DOPunchScale(GameplaySettings.Settings.SquishAmount, 
                                                  GameplaySettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(GameplaySettings.Settings.SquishEase);
        }

        public void OnClick()
        {
            BattleController.Instance.SelectScoreEntry(this);
        }
    }
}

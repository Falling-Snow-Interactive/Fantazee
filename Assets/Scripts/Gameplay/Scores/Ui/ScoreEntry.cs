using System;
using System.Collections.Generic;
using ProjectYahtzee.Dice.Information;
using ProjectYahtzee.Dice.Settings;
using ProjectYahtzee.Gameplay.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectYahtzee.Gameplay.Scores.Ui
{
    public class ScoreEntry : MonoBehaviour
    {
        [SerializeField]
        private ScoreType type;
        public ScoreType Type => type;
        
        [Header("References")]
        
        [SerializeField]
        private TMP_Text tmp;
        
        [SerializeField]
        private TMP_Text score;

        [SerializeField]
        private Button button;

        [SerializeField]
        private List<Image> diceImages = new();
        public List<Image> DiceImages => diceImages;

        private void Start()
        {
            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(type, out var information))
            {
                if (tmp)
                {
                    tmp.text = information.LocName.GetLocalizedString();
                }

                SetDice(new List<int> {0, 0, 0, 0, 0 });
            }
        }

        public void Initialize(ScoreType type)
        {
            this.type = type;
            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(type, out var information))
            {
                if (tmp)
                {
                    tmp.text = information.LocName.GetLocalizedString();
                }

                SetDice(new List<int> {0, 0, 0, 0, 0 });
            }
        }

        public void SetDice(List<int> values)
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

        public void SetDice(List<Dices.Dice> diceList)
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
        
        public void SetDice(int index, int value)
        {
            diceImages[index].gameObject.SetActive(true);
            if (DiceSettings.Settings.SideInformation.TryGetInformation(value, out SideInformation information))
            {
                diceImages[index].sprite = information.Sprite;
            }
        }

        public void SetScore(int value)
        {
            button.interactable = false;
            score.text = value.ToString();
        }

        public void OnClick()
        {
            GameplayController.Instance.SelectScoreEntry(this);
        }
    }
}

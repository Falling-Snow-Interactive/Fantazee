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

        public void Initialize(ScoreType type)
        {
            this.type = type;
            if (GameplaySettings.Settings.ScoreInformation.TryGetInformation(type, out var information))
            {
                if (tmp)
                {
                    tmp.text = information.LocName.GetLocalizedString();
                }
                
                switch (type)
                {
                    case ScoreType.None:
                        break;
                    case ScoreType.Ones:
                        SetDice(new List<int>{1,1,1,1,1});
                        break;
                    case ScoreType.Twos:
                        SetDice(new List<int>{2,2,2,2,2});
                        break;
                    case ScoreType.Threes:
                        SetDice(new List<int>{3,3,3,3,3});
                        break;
                    case ScoreType.Fours:
                        SetDice(new List<int>{4,4,4,4,4});
                        break;
                    case ScoreType.Fives:
                        SetDice(new List<int>{5,5,5,5,5});
                        break;
                    case ScoreType.Sixes:
                        SetDice(new List<int>{6,6,6,6,6});
                        break;
                    case ScoreType.ThreeOfAKind:
                        SetDice(new List<int> { 4,4,4 });
                        break;
                    case ScoreType.FourOfAKind:
                        SetDice(new List<int> { 5,5,5,5 });
                        break;
                    case ScoreType.FullHouse:
                        SetDice(new List<int> { 5,5,5, 3,3 });
                        break;
                    case ScoreType.SmallStraight:
                        SetDice(new List<int> { 1, 2, 3, 4 });
                        break;
                    case ScoreType.LargeStraight:
                        SetDice(new List<int> { 2, 3, 4, 5, 6 });
                        break;
                    case ScoreType.Yahtzee:
                        SetDice(new List<int> { 6,6,6,6,6 });
                        break;
                    case ScoreType.Chance:
                        SetDice(new List<int> { 5,3,4,1,6 });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
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

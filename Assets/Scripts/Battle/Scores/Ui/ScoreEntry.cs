using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using ProjectYahtzee.Battle.Scores.Information;
using ProjectYahtzee.Battle.Settings;
using ProjectYahtzee.Dice.Information;
using ProjectYahtzee.Dice.Settings;
using TMPro;
using UnityEngine;
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
        
        [SerializeField]
        private TMP_Text valueText;
        
        [SerializeField]
        private Transform valueContainer;
        
        [SerializeField]
        private TMP_Text modText;
        
        [SerializeField]
        private Transform modContainer;

        [SerializeField]
        private Button button;

        [SerializeField]
        private List<Image> diceImages = new();
        public List<Image> DiceImages => diceImages;
        
        private ScoreInformation information;

        // private void Start()
        // {
        //     Initialize(score);
        // }

        public void Initialize(Score score)
        {
            this.score = score;
            
            valueText.text = score.Value.ToString(CultureInfo.InvariantCulture);
            modText.text = score.Mod.ToString(CultureInfo.InvariantCulture);
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
            nameText.text = value.ToString();
            nameText.transform.DOPunchScale(Vector3.one * 1.1f, 0.25f, 10, 1f);
        }

        public void OnClick()
        {
            BattleController.Instance.SelectScoreEntry(this);
        }

        public void SetValue(float value)
        {
            valueText.text = value.ToString(CultureInfo.InvariantCulture);
            valueContainer.DOPunchScale(Vector3.one * 1.1f, 0.25f, 10, 1f);
        }

        public void SetMod(float mod)
        {
            modText.text = mod.ToString(CultureInfo.InvariantCulture);
            modContainer.DOPunchScale(Vector3.one * 1.1f, 0.25f, 10, 1f);

        }
    }
}

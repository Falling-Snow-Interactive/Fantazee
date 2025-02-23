using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Items.Dice.Randomizer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Blacksmith.Ui
{
    public class BlacksmithSideUi : MonoBehaviour
    {
        public Die Die { get; private set; }
        public DieRandomizerEntry Side { get; private set; }

        private BlacksmithSideGroupUi groupUi;
        
        [SerializeField]
        private BlacksmithDieUi dieUi;

        [SerializeField]
        private TMP_Text weightText;

        [SerializeField]
        private List<Image> weightImages = new();
        
        public void Initialize(Die die, DieRandomizerEntry side, BlacksmithSideGroupUi groupUi)
        {
            Die = die;
            Side = side;
            dieUi.SetImage(side.Value);
            weightText.text = side.Weight.ToString();

            SetWeight(side.Weight);
            
            this.groupUi = groupUi;
        }

        public void SetWeight(float weight)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i < weight)
                {
                    Color color = weightImages[i].color;
                    color.a = 1f;
                    weightImages[i].color = color;
                }
                else
                {
                    Color color = weightImages[i].color;
                    color.a = 0.2f;
                    weightImages[i].color = color;
                }
            }
        }

        public void SetImage(int value)
        {
            dieUi.SetImage(value);
        }
        
        public void OnValuePlus()
        {
            groupUi.UpgradeDieValue(this, Side.Value, 1);
        }

        public void OnValueMinus()
        {
            groupUi.UpgradeDieValue(this, Side.Value, -1);
        }

        public void OnWeightPlus()
        {
            groupUi.UpgradeDieWeight(this, Side.Value, 1);
        }

        public void OnWeightMinus()
        { 
            groupUi.UpgradeDieWeight(this, Side.Value, -1);
        }
    }
}

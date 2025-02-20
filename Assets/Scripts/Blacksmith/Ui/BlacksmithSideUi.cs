using System.Globalization;
using Fantazhee.Items.Dice;
using Fantazhee.Items.Dice.Ui;
using Fantazhee.Dice;
using Fantazhee.Items.Dice.Randomizer;
using TMPro;
using UnityEngine;

namespace Fantazhee.Blacksmith.Ui
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
        
        public void Initialize(Die die, DieRandomizerEntry side, BlacksmithSideGroupUi groupUi)
        {
            Die = die;
            Side = side;
            dieUi.SetImage(side.Value);
            weightText.text = side.Weight.ToString();
            
            this.groupUi = groupUi;
        }

        public void SetWeight(float weight)
        {
            weightText.text = weight.ToString(CultureInfo.InvariantCulture);
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

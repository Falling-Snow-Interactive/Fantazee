using System.Collections.Generic;
using ProjectYahtzee.Dice;
using ProjectYahtzee.Gameplay.Dice;
using ProjectYahtzee.Gameplay.Ui.Dice;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Gameplay.Ui.DiceControl
{
    public class DiceControlUi : MonoBehaviour
    {
        [FormerlySerializedAs("dicePrefab")]
        [SerializeField]
        private DiceUi diceUiPrefab;

        [SerializeField]
        private List<DiceUi> currentDice = new();
        
        [SerializeField]
        private Transform diceContainer;
        
        public void DrawDice(int diceCount)
        {
            for (int i = 0; i < diceCount; i++)
            {
                DiceUi d = Instantiate(diceUiPrefab, diceContainer);
                currentDice.Add(d);
            }
        }

        public void TryRoll()
        {
            int i = 0;
            foreach (DiceUi d in currentDice)
            {
                if (!d.Locked)
                {
                    d.Roll(i * 0.2f);
                    i++;
                }
            }
        }
    }
}
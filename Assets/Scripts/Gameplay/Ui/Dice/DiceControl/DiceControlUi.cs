using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ProjectYahtzee.Gameplay.Ui.Dice.DiceControl
{
    public class DiceControlUi : MonoBehaviour
    {
        [FormerlySerializedAs("dicePrefab")]
        [SerializeField]
        private DiceUi diceUiPrefab;

        [SerializeField]
        private List<DiceUi> currentDice = new();
        public List<DiceUi> CurrentDice => currentDice;
        
        [SerializeField]
        private Transform diceContainer;

        [SerializeField]
        private int numberOfRolls = 3;

        [SerializeField]
        private int rollsRemaining = 3;
        
        public void DrawDice(int diceCount)
        {
            rollsRemaining = numberOfRolls;
            for (int i = 0; i < diceCount; i++)
            {
                DiceUi d = Instantiate(diceUiPrefab, diceContainer);
                currentDice.Add(d);
            }
        }

        public void TryRoll()
        {
            if (rollsRemaining > 0)
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

                rollsRemaining--;
            }
        }

        public void ClearDice()
        {
            foreach (var d in currentDice)
            {
                Destroy(d.gameObject);
            }

            currentDice.Clear();
        }
    }
}
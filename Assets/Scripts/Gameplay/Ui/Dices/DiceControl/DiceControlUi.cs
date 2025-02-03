using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui.Dices.DiceControl
{
    public class DiceControlUi : MonoBehaviour
    {
        [SerializeField]
        private List<DiceUi> dice = new();
        public List<DiceUi> Dice => dice;

        public void Roll()
        {
            int i = 0;
            foreach (DiceUi d in dice)
            {
                d.gameObject.SetActive(true);
                if (!d.Dice.Locked)
                {
                    d.Roll(i * 0.2f);
                    i++;
                }
            }
        }

        public void HideDice()
        {
            foreach (var d in dice)
            {
                d.Hide();
            }
        }
    }
}
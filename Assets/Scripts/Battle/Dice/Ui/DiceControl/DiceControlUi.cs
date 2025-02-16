using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectYahtzee.Battle.Dice.Ui.DiceControl
{
    public class DiceControlUi : MonoBehaviour
    {
        [SerializeField]
        private List<DieUi> dice = new();
        public List<DieUi> Dice => dice;

        public void Roll(Action<Die> onRollComplete)
        {
            int i = 0;
            foreach (DieUi d in dice)
            {
                if (!d.Die.Locked)
                {
                    d.Roll(i * 0.2f, onRollComplete);
                    i++;
                }
            }
        }

        public void HideDice(Action onComplete)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                d.Hide(onComplete, i * 0.2f, false);
            }
        }
    }
}
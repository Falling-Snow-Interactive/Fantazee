using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ProjectYahtzee.Battle.Ui.Dices.DiceControl
{
    public class DiceControlUi : MonoBehaviour
    {
        [SerializeField]
        private List<DiceUi> dice = new();
        public List<DiceUi> Dice => dice;

        public void Roll(Action<Battle.Dices.Dice> onRollComplete)
        {
            int i = 0;
            foreach (DiceUi d in dice)
            {
                if (!d.Dice.Locked)
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
                DiceUi d = dice[i];
                d.Hide(onComplete, i * 0.2f, false);
            }
        }
    }
}
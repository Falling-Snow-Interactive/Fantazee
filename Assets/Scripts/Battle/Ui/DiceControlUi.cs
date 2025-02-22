using System;
using System.Collections.Generic;
using Fantazee.Dice;
using Fantazee.Dice.Ui;
using Fantazee.Items.Dice;
using UnityEngine;

namespace Fantazee.Battle.Ui
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
                if (!BattleController.Instance.LockedDice.Contains(d.Die))
                {
                    d.Roll(i * 0.2f, onRollComplete);
                    i++;
                }
            }
        }

        public void HideDice(Action onComplete = null)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                if (i == dice.Count - 1)
                {
                    d.Hide(onComplete, i * 0.2f, false);
                }
                else
                {
                    d.Hide(null, i * 0.2f, false);
                }
            }
        }

        public void ResetDice()
        {
            foreach (DieUi d in BattleUi.Instance.DiceControl.Dice)
            {
                d.ResetDice();
            }
        }

        public void ShowDice(Action onComplete = null)
        {
            for (int i = 0; i < dice.Count; i++)
            {
                DieUi d = dice[i];
                d.Show(i == dice.Count - 1 ? onComplete : null, i * 0.2f, false);
            }
        }
    }
}
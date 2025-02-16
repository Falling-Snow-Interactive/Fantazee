using System;
using System.Collections.Generic;
using ProjectYahtzee.Items.Dice;
using ProjectYahtzee.Items.Dice.Ui;
using UnityEngine;

namespace ProjectYahtzee.Battle.Ui
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
                if (i == dice.Count - 1)
                {
                    d.Show(onComplete, i * 0.2f, false);
                }
                else
                {
                    d.Show(null, i * 0.2f, false);
                }
            }
        }
    }
}
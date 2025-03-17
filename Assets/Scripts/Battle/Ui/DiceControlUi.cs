using System;
using System.Collections.Generic;
using Fantazee.Battle.Characters.Player;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="onDieRollComplete">Called for each die.</param>
        /// <param name="onDiceRollsComplete">Called when all dice have finished rolling.</param>
        public void Roll(BattlePlayer player, Action<Die> onDieRollComplete, Action onDiceRollsComplete)
        {
            int i = 0;
            foreach (DieUi d in dice)
            {
                if (!player.LockedDice.Contains(d.Die))
                {
                    d.Roll(i * 0.2f, die =>
                                     {
                                         i--;
                                         onDieRollComplete?.Invoke(die);
                                         if (i == 0)
                                         {
                                             onDiceRollsComplete?.Invoke();
                                         }
                                     });
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
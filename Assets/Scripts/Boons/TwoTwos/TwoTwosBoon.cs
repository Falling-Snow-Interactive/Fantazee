using System;
using DG.Tweening;
using Fantahzee.Battle.Ui;
using Fantahzee.Boons.Handlers;
using Fantahzee.Dice;
using Fantahzee.Items.Dice.Ui;
using Fantahzee.Items.Dice;
using UnityEngine;

namespace Fantahzee.Boons.TwoTwos
{
    [Serializable]
    public class TwoTwosBoon : Boon, IBoonRollHandler
    {
        public override BoonType Type => BoonType.TwoTwos;
        public override string GetBonusText() => "";

        public void OnDiceRoll(Die die)
        {
            if (die.Value == 2)
            {
                DieUi dieUi = null;
                foreach (DieUi d in BattleUi.Instance.DiceControl.Dice)
                {
                    if (d.Die == die)
                    {
                        dieUi = d;
                    }
                }

                if (dieUi)
                {
                    dieUi.transform.DOShakeRotation(0.3f, new Vector3(0, 0, 25f), 25, 150, true, ShakeRandomnessMode.Full)
                          .OnComplete(() =>
                                      {
                                          die.Value = 4;
                                          dieUi.UpdateImage();
                                          entryUi.Squish();
                                      });
                }
            }
        }
    }
}
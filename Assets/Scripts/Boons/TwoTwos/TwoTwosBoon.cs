using System;
using DG.Tweening;
using Fantazhee.Items.Dice;
using Fantazhee.Battle.Ui;
using Fantazhee.Boons.Handlers;
using Fantazhee.Dice;
using Fantazhee.Items.Dice.Ui;
using UnityEngine;

namespace Fantazhee.Boons.TwoTwos
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
using System;
using DG.Tweening;
using ProjectYahtzee.Battle.Dice.Ui;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Boons.Handlers;
using UnityEngine;

namespace ProjectYahtzee.Boons.TwoTwos
{
    [Serializable]
    public class TwoTwosBoon : Boon, IBoonRollHandler
    {
        public override BoonType Type => BoonType.TwoTwos;
        public override string GetBonusText() => "";

        public void OnDiceRoll(Battle.Dice.Die die)
        {
            if (die.Value == 2)
            {
                DieUi dieUi = null;
                foreach (DieUi d in GameplayUi.Instance.DiceControl.Dice)
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
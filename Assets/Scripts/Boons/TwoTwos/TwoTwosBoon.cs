using DG.Tweening;
using ProjectYahtzee.Battle.Dices.Ui;
using ProjectYahtzee.Battle.Ui;
using ProjectYahtzee.Boons.Handlers;
using UnityEngine;

namespace ProjectYahtzee.Boons.TwoTwos
{
    public class TwoTwosBoon : Boon, IBoonRollHandler
    {
        public override BoonType Type => BoonType.TwoTwos;
        public override string GetBonusText() => "";

        public void OnDiceRoll(Battle.Dices.Dice dice)
        {
            if (dice.Value == 2)
            {
                DiceUi diceUi = null;
                foreach (var d in GameplayUi.Instance.DiceControl.Dice)
                {
                    if (d.Dice == dice)
                    {
                        diceUi = d;
                    }
                }

                if (diceUi != null)
                {
                    diceUi.transform.DOShakeRotation(0.3f, new Vector3(0, 0, 25f), 25, 150, true, ShakeRandomnessMode.Full)
                          .OnComplete(() =>
                                      {
                                          dice.Value = 4;
                                          diceUi.UpdateImage();
                                          entryUi.Squish();
                                      });
                }
            }
        }
    }
}
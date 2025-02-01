using Fsi.Gameplay;
using ProjectYahtzee.Gameplay.Ui.Dice.DiceControl;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Ui
{
    public class GameplayUi : MbSingleton<GameplayUi>
    {
        [SerializeField]
        private DiceControlUi diceControl;
        public DiceControlUi DiceControl => diceControl;
    }
}
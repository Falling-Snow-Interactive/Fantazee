using Fsi.Gameplay;
using ProjectYahtzee.Dice;
using ProjectYahtzee.Gameplay.Ui.DiceControl;
using UnityEngine;

namespace ProjectYahtzee.Gameplay
{
    public class GameplayController : MbSingleton<GameplayController>
    {
        [Header("Camera")]
        
        [SerializeField]
        private new Camera camera;

        private Camera Camera
        {
            get
            {
                if (camera == null)
                {
                    camera = Camera.main;
                }

                return camera;
            }
        }

        [Header("Dice")]

        [SerializeField]
        private DiceControlUi diceControl;

        private void Start()
        {
            diceControl.DrawDice(5);
            diceControl.TryRoll();
        }
    }
}

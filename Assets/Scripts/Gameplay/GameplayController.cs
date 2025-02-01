using Fsi.Gameplay;
using ProjectYahtzee.Gameplay.Score.Ui;
using ProjectYahtzee.Gameplay.Ui.Dice.DiceControl;
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
        
        [SerializeField]
        private ScoreboardUi scoreboard;

        private void Start()
        {
            diceControl.DrawDice(5);
            diceControl.TryRoll();
        }

        public void SelectScoreEntry(ScoreEntry entry)
        {
            scoreboard.SetScore(entry.Type, diceControl.CurrentDice);
            diceControl.ClearDice();
            diceControl.DrawDice(5);
            diceControl.TryRoll();
        }
    }
}

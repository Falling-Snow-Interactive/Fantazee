using System.Collections.Generic;
using Fsi.Gameplay;
using ProjectYahtzee.Gameplay.Scores;
using ProjectYahtzee.Gameplay.Scores.Ui;
using ProjectYahtzee.Gameplay.Ui;
using ProjectYahtzee.Gameplay.Ui.Dices;
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
        private List<Dices.Dice> dice = new();

        [Header("Roll")]

        [SerializeField]
        private int rolls = 3;

        [SerializeField]
        private int remainingRolls = 3;

        [Header("Score")]

        [SerializeField]
        private Score score;
        public Score Score => score;

        private void Start()
        {
            score.Initialize();
            
            for (int i = 0; i < this.dice.Count; i++)
            {
                Dices.Dice dice = this.dice[i];
                if (GameplayUi.Instance.DiceControl.Dice.Count > i)
                {
                    DiceUi diceUi = GameplayUi.Instance.DiceControl.Dice[i];
                    diceUi.Initialize(dice);
                }
            }
            
            StartTurn();
        }
        
        private void StartTurn()
        {
            remainingRolls = rolls;
            foreach (Dices.Dice d in this.dice)
            {
                d.Locked = false;
            }
            TryRoll();
        }

        public void SelectScoreEntry(ScoreEntry entry)
        {
            if (score.CanScore(entry.Type))
            {
                score.AddScore(entry.Type, dice);
                GameplayUi.Instance.Scoreboard.PlayScoreSequence(entry, 
                                                                 GameplayUi.Instance.DiceControl.Dice, 
                                                                 () =>
                                                                 {
                                                                     GameplayUi.Instance.Scoreboard
                                                                               .SetScore(entry.Type, dice);
                                                                     CheckBoard();
                                                                 });
            }
        }

        public void TryRoll()
        {
            if (remainingRolls > 0)
            {
                remainingRolls--;
                foreach (Dices.Dice d in dice)
                {
                    if (!d.Locked)
                    {
                        d.Roll();
                    }
                }
                
                GameplayUi.Instance.DiceControl.Roll();
            }
        }

        private void CheckBoard()
        {
            StartTurn();
        }
    }
}

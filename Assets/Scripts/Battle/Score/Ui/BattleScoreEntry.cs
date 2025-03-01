using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Settings;
using Fantazee.Dice.Settings;
using Fantazee.Scores.Ui;
using Fantazee.Scores.Ui.ScoreEntries;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Battle.Score.Ui
{
    public class BattleScoreEntry : ScoreEntry
    {
        [FormerlySerializedAs("score")]
        [SerializeReference]
        private BattleScore battleScore;
        public BattleScore BattleScore => battleScore;
        
        private void OnEnable()
        {
            if (battleScore != null)
            {
                battleScore.DieAdded += OnDieAdded;
                battleScore.ScoreReset += OnBattleScoreReset;
            }
        }

        private void OnDisable()
        {
            if (battleScore != null)
            {
                battleScore.DieAdded -= OnDieAdded;
                battleScore.ScoreReset -= OnBattleScoreReset;
            }
        }

        public void Initialize(BattleScore battleScore, Action<ScoreEntry> onSelect)
        {
            this.battleScore = battleScore;
            base.Initialize(battleScore.Score, onSelect);
            
            battleScore.DieAdded += OnDieAdded;
            battleScore.ScoreReset += OnBattleScoreReset;
        }

        protected override List<int> GetDiceValues()
        {
            return new List<int>{0,0,0,0,0};
        }

        public int FinalizeScore()
        {
            int s = battleScore.Calculate();
            button.interactable = false;
            scoreText.text = s.ToString();
            RuntimeManager.PlayOneShot(BattleSettings.Settings.ScoreSfx);
            scoreContainer.transform.DOPunchScale(DiceSettings.Settings.SquishAmount, 
                                                  DiceSettings.Settings.SquishTime,
                                                  10,
                                                  1f)
                          .SetEase(DiceSettings.Settings.SquishEase);

            return s;
        }
        
        private void OnDieAdded()
        {
            for (int i = 0; i < battleScore.Dice.Count; i++)
            {
                ShowDieInSlot(i, battleScore.Dice[i].Value);
            }
        }

        private void OnBattleScoreReset()
        {
            for (int i = 0; i < diceImages.Count; i++)
            {
                int v = battleScore.Dice.Count > i ? battleScore.Dice[i].Value : 0;
                ShowDieInSlot(i, v);
            }

            button.interactable = battleScore.Dice.Count == 0;
            scoreText.text = battleScore.Dice.Count == 0 ? "" : battleScore.Calculate().ToString();
        }
    }
}

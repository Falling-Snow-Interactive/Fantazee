using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Environments.Information;
using Fantazee.Battle.Settings;
using Fantazee.Dice;
using Fantazee.Environments.Information;
using Fantazee.Environments.Settings;
using Fantazee.Scores.Bonus.Ui;
using Fantazee.Scores.Ui.ScoreEntries;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Battle.Score.Ui
{
    public class ScoreboardUi : MonoBehaviour
    {
        private Action<ScoreEntry> onSelect;
        
        [Header("Animation")]
        
        [SerializeField]
        private float scoreTime = 0.6f;
        
        [SerializeField]
        private Ease scoreEase = Ease.InCirc;
        
        [Header("Prefabs")]

        [FormerlySerializedAs("scoreEntryPrefab")]
        [SerializeField]
        private BattleScoreEntry battleScoreEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform entryContainer;
        
        [SerializeField]
        private List<BattleScoreEntry> entries = new List<BattleScoreEntry>();

        [SerializeField]
        private BattleScoreEntry fantazeeEntry;

        [SerializeField]
        private BonusScoreUi bonusScoreUi;
        public BonusScoreUi BonusScoreUi => bonusScoreUi;

        [SerializeField]
        private Image background;

        private void OnEnable()
        {
            BattleController.RollEnded += OnRollEnded;
            BattleController.Scored += OnScored;
            BattleController.RollStarted += OnRollStarted;
        }

        private void OnDisable()
        {
            BattleController.RollEnded -= OnRollEnded;
            BattleController.Scored -= OnScored;
            BattleController.RollStarted -= OnRollStarted;
        }

        public void Initialize(List<BattleScore> battleScores, BattleScore fantazeeScore, Action<ScoreEntry> onSelect)
        {
            this.onSelect = onSelect;
            
            for (int i = 0; i < battleScores.Count; i++)
            {
                BattleScore score = battleScores[i];
                BattleScoreEntry entry = entries[i];
                entry.Initialize(score, OnScoreEntrySelected);
                score.SetEntry(entry);
            }
            
            // TODO - Fix fantazee entry
            fantazeeEntry.Initialize(fantazeeScore, OnScoreEntrySelected);
            fantazeeScore.SetEntry(fantazeeEntry);
            
            if (EnvironmentSettings.Settings.Information.TryGetInformation(GameController.Instance.GameInstance.Map.Environment, 
                                                                           out EnvironmentInformation info))
            {
                background.color = info.Color;
            }
        }

        private void OnScoreEntrySelected(ScoreEntry scoreEntry)
        {
            onSelect?.Invoke(scoreEntry);
        }

        private void OnRollEnded()
        {
            ShowScorePreviews();
        }

        private void OnScored(BattleScore _)
        {
            HideScorePreviews();
        }

        private void ShowScorePreviews()
        {
            foreach (BattleScoreEntry entry in entries)
            {
                entry.ShowPreview();
            }
        }

        private void HideScorePreviews()
        {
            foreach (BattleScoreEntry entry in entries)
            {
                entry.HidePreview();
            }
        }

        private void OnRollStarted()
        {
            HideScorePreviews();
        }
    }
}
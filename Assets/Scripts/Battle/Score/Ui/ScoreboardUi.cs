using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Environments.Information;
using Fantazee.Battle.Settings;
using Fantazee.Scores.Bonus.Ui;
using UnityEngine;
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

        [SerializeField]
        private ScoreEntry scoreEntryPrefab;
        
        [Header("References")]
        
        [SerializeField]
        private Transform entryContainer;
        
        [SerializeField]
        private List<ScoreEntry> entries = new List<ScoreEntry>();

        [SerializeField]
        private ScoreEntry fantazeeEntry;

        [SerializeField]
        private BonusScoreUi bonusScoreUi;
        public BonusScoreUi BonusScoreUi => bonusScoreUi;

        [SerializeField]
        private Image background;

        public void Initialize(List<BattleScore> battleScores, BattleScore fantazeeScore, Action<ScoreEntry> onSelect)
        {
            this.onSelect = onSelect;
            
            for (int i = 0; i < battleScores.Count; i++)
            {
                BattleScore score = battleScores[i];
                ScoreEntry entry = entries[i];
                entry.Initialize(score, OnScoreEntrySelected);
                score.SetEntry(entry);
            }
            
            // TODO - Fix fantazee entry
            fantazeeEntry.Initialize(fantazeeScore, OnScoreEntrySelected);
            
            if (BattleSettings.Settings.EnvironmentInformation
                              .TryGetInformation(GameController.Instance.GameInstance.Map.Environment, 
                                                 out EnvironmentInformation info))
            {
                background.color = info.Color;
            }
        }

        private void OnScoreEntrySelected(ScoreEntry scoreEntry)
        {
            onSelect?.Invoke(scoreEntry);
        }
    }
}
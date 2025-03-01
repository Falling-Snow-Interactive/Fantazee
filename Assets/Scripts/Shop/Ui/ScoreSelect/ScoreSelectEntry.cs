using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Settings;
using Fantazee.Scores;
using Fantazee.Scores.Information;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.ScoreSelect
{
    public class ScoreSelectEntry : MonoBehaviour
    {
        private Action<ScoreSelectEntry> onSelect;
        
        [SerializeReference]
        private Score score;
        public Score Score
        {
            get => score;
            set => score = value;
        }

        [Header("References")]

        [SerializeField]
        private List<ScoreEntrySpell> spells = new();

        [SerializeField]
        private TMP_Text scoreText;

        public void Initialize(Score score, Action<ScoreSelectEntry> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;

            UpdateVisuals();
        }

        public void UpdateVisuals()
        {
            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score.Type, out ScoreInformation scoreInfo))
            {
                scoreText.text = scoreInfo.LocName.GetLocalizedString();
            }

            for (int i = 0; i < score.Spells.Count; i++)
            {
                spells[i].Initialize(i, score.Spells[i]);
            }
        }

        public void Select()
        {
            onSelect?.Invoke(this);
        }
    }
}

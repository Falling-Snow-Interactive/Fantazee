using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Ui;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopSpellScreen : ShopScreen
    {
        private Action onComplete;
        
        [Header("Score References")]
        
        [SerializeField]
        private SpellEntry entry;

        [SerializeField]
        private List<ScoreSelectEntry> scoreEntries = new();
        
        private void Awake()
        {
            transform.localPosition = localOut;
        }

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            
            this.onComplete = onComplete;
            entry.Initialize(selected.Spell, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreSelectEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
                scoreEntry.Initialize(score, OnScoreSelected);
            }
        }

        private void OnScoreSelected(ScoreSelectEntry scoreEntry)
        {
            Debug.Log("Do stuff");
            onComplete?.Invoke();
        }
    }
}
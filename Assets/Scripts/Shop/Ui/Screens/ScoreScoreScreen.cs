using System;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ScoreScoreScreen : ScoreScreen
    {
        private ScoreType scoreType;
        private ScoreShopEntry selected;
        
        [Header("Score Score References")]
        
        [SerializeField]
        private ScoreShopEntry scoreEntry;

        public void Initialize(ScoreShopEntry selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            
            scoreType = selected.Score;
            this.onComplete = onComplete;
            this.selected = selected;
            
            scoreEntry.gameObject.SetActive(true);
            scoreEntry.transform.localPosition = Vector3.zero;
            scoreEntry.Initialize(selected.Score, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreSelectEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
                scoreEntry.Initialize(score, PlayAnimation);
            }
        }

        protected override bool Apply(ScoreSelectEntry scoreEntry)
        {
            if (!GameInstance.Current.Character.Wallet.Remove(this.scoreEntry.Information.Cost))
            {
                Debug.LogWarning("Shop: Cannot afford spell. Returning to shop.");
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} -> {scoreEntry.Score.Type}");
            scoreEntry.Score.Type = scoreType;
            
            selected.gameObject.SetActive(false);

            return true;
        }
    }
}

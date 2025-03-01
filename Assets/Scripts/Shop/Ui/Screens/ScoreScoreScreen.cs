using System;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ScoreScoreScreen : ScoreScreen
    {
        private ScoreType scoreType;
        private ScoreShopEntry selectedOnMain;
        
        [Header("Score Score References")]
        
        [SerializeField]
        private ShopScoreEntry scoreEntry;

        public void Initialize(ScoreShopEntry selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            
            scoreType = selected.Score;
            this.onComplete = onComplete;
            selectedOnMain = selected;
            
            scoreEntry.gameObject.SetActive(true);
            scoreEntry.transform.localPosition = Vector3.zero;
            
            // TODO - this matches what's selected on the shop main screen
            // scoreEntry.Initialize(selected.Score, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];

                // TODO - probably somrething
                scoreEntry.Initialize(score, se =>
                                             {
                                                 ScoreSelectSequence(se);
                                             });
            }
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!GameInstance.Current.Character.Wallet.Remove(this.scoreEntry.Score.Information.Cost))
            {
                Debug.LogWarning("Shop: Cannot afford spell. Returning to shop.");
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} -> {selectedOnMain.Score}");

            int index = GameInstance.Current.Character.ScoreTracker.Scores.IndexOf(scoreEntry.Score);
            GameInstance.Current.Character.ScoreTracker.Scores[index] = ScoreFactory.Create(selectedOnMain.Score, scoreEntry.Score.Spells);
            scoreEntry.Score = GameInstance.Current.Character.ScoreTracker.Scores[index];
            
            selectedOnMain.gameObject.SetActive(false);

            return true;
        }
    }
}

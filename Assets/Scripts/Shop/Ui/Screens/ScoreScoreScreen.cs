using System;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ScoreScoreScreen : ScoreScreen
    {
        [SerializeField]
        private ShopScoreEntryPurchase purchase;

        [SerializeField]
        private ShopScoreEntryPurchase mainMenuSelected;

        public void Initialize(ShopScoreEntryPurchase selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);

            mainMenuSelected = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            
            purchase.Initialize(selected.Score, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];

                // TODO - probably somrething
                scoreEntry.Initialize(score, se =>
                                             {
                                                 ScoreSelectSequence(purchase.transform, se, onComplete);
                                             });
            }
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Score.Information.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} -> {purchase.Score.Type}");

            int index = GameInstance.Current.Character.ScoreTracker.Scores.IndexOf(scoreEntry.Score);
            GameInstance.Current.Character.ScoreTracker.Scores[index] = ScoreFactory.Create(purchase.Score.Type, scoreEntry.Score.Spells);
            scoreEntry.Score = GameInstance.Current.Character.ScoreTracker.Scores[index];
            
            mainMenuSelected.gameObject.SetActive(false);
            
            return true;
        }
    }
}

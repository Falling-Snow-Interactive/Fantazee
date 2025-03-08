using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using Fantazee.Spells.Instance;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ScoreScoreScreen : ScoreScreen
    {
        [SerializeField]
        private ShopScoreEntryPurchase purchase;

        private ShopScoreEntry selectMain;

        public void Initialize(ShopScoreEntryPurchase selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);

            selectMain = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            purchase.Initialize(selected.Score, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];

                // TODO - probably somrething
                scoreEntry.Initialize(score, se =>
                                             {
                                                 ScoreSelectSequence(purchase.transform, se, onComplete);
                                             });
            }
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Score.Data.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score} -> {purchase.Score}");

            List<SpellInstance> purchaseSpells = new();
            for (int i = 0; i < purchase.Spells.Count; i++)
            {
                SpellInstance s = purchase.Spells[i].Spell.Data.Type != SpellType.None
                                      ? purchase.Spells[i].Spell
                                      : scoreEntry.Spells[i].Spell;
                purchaseSpells.Add(s);
            }

            int index = GameInstance.Current.Character.Scoresheet.Scores.IndexOf(scoreEntry.Score);
            GameInstance.Current.Character.Scoresheet.Scores[index] = ScoreFactory.CreateInstance(purchase.Score.Data, purchaseSpells);
            scoreEntry.Score = GameInstance.Current.Character.Scoresheet.Scores[index];
            
            purchase.gameObject.SetActive(false);

            selectMain.gameObject.SetActive(false);
            
            return true;
        }
    }
}

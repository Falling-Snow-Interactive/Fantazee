using System;
using System.Collections.Generic;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class ScoreScoreScreen : ScoreScreen
    {
        [SerializeField]
        private ShopScoreButtonPurchase purchase;

        private ShopScoreButton selectMain;

        public void Initialize(ShopScoreButtonPurchase selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);

            selectMain = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            purchase.Initialize(selected.Score, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreButton scoreButton = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];

                // TODO - probably somrething
                scoreButton.Initialize(score, se =>
                                             {
                                                 ScoreSelectSequence(purchase.transform, se, onComplete);
                                             });
            }
        }

        protected override bool Apply(ScoreButton scoreButton)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Score.Data.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreButton.Score} -> {purchase.Score}");

            List<SpellInstance> purchaseSpells = new();
            for (int i = 0; i < purchase.Spells.Count; i++)
            {
                SpellInstance s = purchase.Spells[i].Spell.Data.Type != SpellType.spell_none
                                      ? purchase.Spells[i].Spell
                                      : scoreButton.Spells[i].Spell;
                purchaseSpells.Add(s);
            }

            int index = GameInstance.Current.Character.Scoresheet.Scores.IndexOf(scoreButton.Score);
            GameInstance.Current.Character.Scoresheet.Scores[index] = ScoreFactory.CreateInstance(purchase.Score.Data, purchaseSpells);
            scoreButton.Score = GameInstance.Current.Character.Scoresheet.Scores[index];
            
            purchase.gameObject.SetActive(false);

            selectMain.gameObject.SetActive(false);
            
            return true;
        }
    }
}

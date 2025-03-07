using System;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells.Instance;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class SpellScoreScreen : ScoreScreen
    {
        private SpellInstance spell;
        private SpellEntry purchaseSpell;

        private int selectedSpellIndex = -1;
        
        [FormerlySerializedAs("entry")]
        [SerializeField]
        protected SpellEntry purchase;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            spell = selected.Spell;
            purchaseSpell = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            purchase.Initialize(selected.Spell, null);

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];
                
                scoreEntry.Initialize(score, se =>
                                             {
                                                 OnScoreSelected(se, onComplete);
                                             });
            }
        }

        private void OnScoreSelected(ScoreEntry scoreEntry, Action onComplete)
        {
            Transform parent = scoreEntry.transform.parent;
            scoreEntry.transform.SetParent(animGroup);
            fadeImage.raycastTarget = true;
            fadeImage.DOFade(fadeAmount, fadeTime)
                     .SetEase(fadeEase);
            scoreEntry.RequestSpell((i, se) =>
                                    {
                                        OnSpellSelected(i, se, () =>
                                                               {
                                                                   scoreEntry.transform.SetParent(parent);
                                                                   onComplete?.Invoke();
                                                               });
                                    });
        }

        private void OnSpellSelected(int i, ScoreEntry scoreEntry, Action onComplete)
        {
            selectedSpellIndex = i;
            ScoreSelectSequence(purchase.transform, scoreEntry, onComplete);
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score} {scoreEntry.Score.Spells[0]} -> {spell}");
            scoreEntry.Score.Spells[selectedSpellIndex] = spell;
            
            purchaseSpell.gameObject.SetActive(false);

            return true;
        }
    }
}
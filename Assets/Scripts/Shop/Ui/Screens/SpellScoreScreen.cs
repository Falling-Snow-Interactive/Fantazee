using System;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class SpellScoreScreen : ScoreScreen
    {
        private SpellType spellType;
        private SpellEntry selected;

        private int selectedSpellIndex = -1;
        
        [FormerlySerializedAs("entry")]
        [SerializeField]
        protected SpellEntry purchase;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            spellType = selected.Data.Type;
            this.selected = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            purchase.Initialize(selected.Data, null);

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
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
            if (!GameInstance.Current.Character.Wallet.Remove(purchase.Cost))
            {
                Debug.LogWarning("Shop: Cannot afford spell. Returning to shop.");
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} {scoreEntry.Score.Spells[0]} -> {spellType}");
            scoreEntry.Score.Spells[selectedSpellIndex] = spellType;
            
            selected.gameObject.SetActive(false);

            return true;
        }
    }
}
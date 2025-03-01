using System;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using UnityEngine;

namespace Fantazee.Shop.Ui.Screens
{
    public class SpellScoreScreen : ScoreScreen
    {
        private SpellType spellType;
        private SpellEntry selected;

        private int selectedSpellIndex = -1;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            spellType = selected.Data.Type;
            this.onComplete = onComplete;
            this.selected = selected;
            
            entry.gameObject.SetActive(true);
            entry.transform.localPosition = Vector3.zero;
            if (entry is SpellEntry sEntry)
            {
                sEntry.Initialize(selected.Data, null);
            }

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
                scoreEntry.Initialize(score, OnScoreSelected);
            }
        }

        private void OnScoreSelected(ScoreEntry scoreEntry)
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
                                                               });
                                    });
        }

        private void OnSpellSelected(int i, ScoreEntry scoreEntry, Action onComplete)
        {
            selectedSpellIndex = i;
            ScoreSelectSequence(scoreEntry, onComplete);
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!GameInstance.Current.Character.Wallet.Remove(entry.Cost))
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
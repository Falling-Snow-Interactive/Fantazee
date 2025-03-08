using System;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells.Instance;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Shop.Ui.Screens
{
    public class SpellScoreScreen : ScoreScreen
    {
        private SpellEntry purchaseSpell;
        
        [FormerlySerializedAs("entry")]
        [SerializeField]
        protected SpellEntry purchase;

        private SpellInstance spellInstance;

        [SerializeField]
        private ShopScoreEntry fantazeeEntry;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
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
            
            fantazeeEntry.Initialize(GameInstance.Current.Character.Scoresheet.Fantazee, 
                                     se => OnScoreSelected(se, onComplete));
        }

        private void OnScoreSelected(ScoreEntry scoreEntry, Action onComplete)
        {
            Debug.Log($"{scoreEntry.name}");
            Transform parent = scoreEntry.transform.parent;
            scoreEntry.transform.SetParent(animGroup);
            fadeImage.raycastTarget = true;
            fadeImage.DOFade(ShopSettings.Settings.FadeAmount, ShopSettings.Settings.FadeTime)
                     .SetEase(ShopSettings.Settings.FadeEase);
            scoreEntry.RequestSpell((spellInstance, se) =>
                                    {
                                        this.spellInstance = spellInstance;
                                        OnSpellSelected(se, () =>
                                                            {
                                                                scoreEntry.transform.SetParent(parent);
                                                                onComplete?.Invoke();
                                                            });
                                    });
        }

        private void OnSpellSelected(ScoreEntry scoreEntry, Action onComplete)
        {
            ScoreSelectSequence(purchase.transform, scoreEntry, onComplete);
        }

        protected override bool Apply(ScoreEntry scoreEntry)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreEntry.Score} {spellInstance} -> {purchaseSpell.Spell}");
            int index = scoreEntry.Score.Spells.IndexOf(spellInstance);
            scoreEntry.Score.Spells[index] = purchaseSpell.Spell;
            
            purchaseSpell.gameObject.SetActive(false);

            return true;
        }
    }
}
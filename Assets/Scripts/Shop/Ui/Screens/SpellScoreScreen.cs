using System;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Spells;
using Fantazee.Spells.Ui;
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

        [FormerlySerializedAs("fantazeeEntry")]
        [SerializeField]
        private ShopScoreButton fantazeeButton;

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            purchaseSpell = selected;
            
            purchase.gameObject.SetActive(true);
            purchase.transform.localPosition = Vector3.zero;
            purchase.Initialize(selected.Spell, null);

            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.Scoresheet.Scores.Count);
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ShopScoreButton scoreButton = scoreEntries[i];
                ScoreInstance score = GameInstance.Current.Character.Scoresheet.Scores[i];
                
                scoreButton.Initialize(score, se =>
                                             {
                                                 OnScoreSelected(se, onComplete);
                                             });
            }
            
            fantazeeButton.Initialize(GameInstance.Current.Character.Scoresheet.Fantazee, 
                                     se => OnScoreSelected(se, onComplete));
        }

        private void OnScoreSelected(ScoreButton scoreButton, Action onComplete)
        {
            Transform parent = scoreButton.transform.parent;
            scoreButton.transform.SetParent(animGroup);
            fadeImage.raycastTarget = true;
            // fadeImage.DOFade(ShopSettings.Settings.FadeAmount, ShopSettings.Settings.FadeTime)
            //          .SetEase(ShopSettings.Settings.FadeEase);
            scoreButton.RequestSpell(spellButton =>
                                    {
                                        OnSpellSelected(scoreButton, spellButton, () =>
                                                            {
                                                                scoreButton.transform.SetParent(parent);
                                                                onComplete?.Invoke();
                                                            });
                                    });
        }

        private void OnSpellSelected(ScoreButton scoreButton, SpellButton spellButton, Action onComplete)
        {
            // TODO - Do the right spell, probably somrthing here
            ScoreSelectSequence(purchase.transform, scoreButton, onComplete);
        }

        protected override bool Apply(ScoreButton scoreButton)
        {
            if (!ShopController.Instance.MakePurchase(purchase.Cost))
            {
                return false;
            }
            
            Debug.Log($"Shop Spell: {scoreButton.Score} {spellInstance} -> {purchaseSpell.Spell}");
            int index = scoreButton.Score.Spells.IndexOf(spellInstance);
            scoreButton.Score.Spells[index] = purchaseSpell.Spell;
            
            purchaseSpell.gameObject.SetActive(false);

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Instance;
using Fantazee.Scores;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using Fantazee.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Screens
{
    public class ShopSpellScreen : ShopScreen
    {
        private Action onComplete;

        private SpellType spellType;
        
        [Header("Select animation")]
        
        [SerializeField]
        private float selectTime = 0.6f;

        [SerializeField]
        private Ease selectEase = Ease.InCubic;

        [SerializeField]
        private float entryTime = 0.5f;
        
        [SerializeField]
        private Ease entryEase = Ease.InCubic;

        [SerializeField]
        private float entryOffset = -150f;

        [SerializeField]
        private float punchAmount = 250f;

        [SerializeField]
        private float punchTime = 0.25f;

        [SerializeField]
        private float returnTime = 1f;
        
        [SerializeField]
        private Ease returnEase = Ease.InCubic;

        [SerializeField]
        private float fadeAmount = 0.6f;
        
        [SerializeField]
        private float fadeTime = 0.5f;
        
        [SerializeField]
        private Ease fadeEase = Ease.InOutCubic;
        
        [Header("Score References")]
        
        [SerializeField]
        private SpellEntry entry;

        [SerializeField]
        private List<ScoreSelectEntry> scoreEntries = new();

        [SerializeField]
        private Transform selectedSocket;

        [SerializeField]
        private Transform animGroup;

        [SerializeField]
        private Image fadeImage;
        
        private void Awake()
        {
            transform.localPosition = localOut;
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
        }

        public void Initialize(SpellEntry selected, Action onComplete)
        {
            Debug.Assert(scoreEntries.Count == GameInstance.Current.Character.ScoreTracker.Scores.Count);
            
            spellType = selected.Spell;
            this.onComplete = onComplete;
            
            entry.gameObject.SetActive(true);
            entry.transform.localPosition = Vector3.zero;
            entry.Initialize(selected.Spell, null);
            
            for (int i = 0; i < scoreEntries.Count; i++)
            {
                ScoreSelectEntry scoreEntry = scoreEntries[i];
                Score score = GameInstance.Current.Character.ScoreTracker.Scores[i];
                
                scoreEntry.Initialize(score, OnScoreSelected);
            }
        }

        private void OnScoreSelected(ScoreSelectEntry scoreEntry)
        {
            Debug.Log($"Shop Spell: {scoreEntry.Score.Type} {scoreEntry.Score.Spell} -> {spellType}");
            scoreEntry.Score.Spell = spellType;

            Vector3 pos = scoreEntry.transform.position;
            Transform parent = scoreEntry.transform.parent;
            scoreEntry.transform.SetParent(animGroup, true);
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(scoreEntry.transform.DOMove(selectedSocket.position, selectTime)
                                      .SetEase(selectEase)
                                      .SetDelay(0.15f));
            sequence.Insert(0, fadeImage.DOFade(fadeAmount, fadeTime).SetEase(fadeEase));
            sequence.Append(entry.transform.DOMove(selectedSocket.position + Vector3.right * entryOffset, entryTime)
                                 .SetEase(entryEase)
                                 .SetDelay(0.3f)
                                 .OnComplete(() =>
                                             {
                                                 entry.gameObject.SetActive(false);
                                                 scoreEntry.UpdateSpell();
                                             }));
            sequence.Append(scoreEntry.transform.DOPunchPosition(Vector3.right * punchAmount, punchTime));
            sequence.Append(scoreEntry.transform.DOMove(pos, returnTime)
                                      .SetEase(returnEase)
                                      .SetDelay(0.3f));
            sequence.AppendInterval(0.5f);
            sequence.Insert(2, fadeImage.DOFade(0, fadeTime)
                                        .SetEase(fadeEase));

            sequence.OnComplete(() =>
                                {
                                    scoreEntry.transform.SetParent(parent);
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
    }
}
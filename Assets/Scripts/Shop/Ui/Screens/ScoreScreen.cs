using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Battle.Score.Ui;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Ui.Entries;
using Fantazee.Shop.Ui.ScoreSelect;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Screens
{
    public abstract class ScoreScreen : ShopScreen
    {
        protected Action onComplete;
        
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
        protected float fadeAmount = 0.6f;
        
        [SerializeField]
        protected float fadeTime = 0.5f;
        
        [SerializeField]
        protected Ease fadeEase = Ease.InOutCubic;
        
        [Header("Score References")]
        
        [SerializeField]
        protected ShopEntryUi entry;
        
        [SerializeField]
        protected List<ShopScoreEntry> scoreEntries = new();

        [SerializeField]
        private Transform selectedSocket;

        [SerializeField]
        protected Transform animGroup;

        [SerializeField]
        protected Image fadeImage;

        protected virtual void Awake()
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
            
            transform.localPosition = hidePos;
        }

        protected abstract bool Apply(ScoreEntry scoreEntry);

        protected void ScoreSelectSequence(ScoreEntry scoreEntry, Action onSequenceComplete = null)
        {
            if (!Apply(scoreEntry))
            {
                Debug.LogWarning("Shop: Cannot apply spell. Returning to shop.");
                onComplete?.Invoke();
                return;
            }

            Vector3 pos = scoreEntry.transform.position;
            Transform parent = scoreEntry.transform.parent;
            scoreEntry.transform.SetParent(animGroup, true);
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(scoreEntry.transform.DOMove(selectedSocket.position, selectTime)
                                      .SetEase(selectEase)
                                      .SetDelay(0.15f));
            sequence.Insert(0, fadeImage.DOFade(fadeAmount, fadeTime)
                                        .SetEase(fadeEase)
                                        .OnStart(() => fadeImage.raycastTarget = true));
            sequence.Append(entry.transform.DOMove(selectedSocket.position + Vector3.right * entryOffset, entryTime)
                                 .SetEase(entryEase)
                                 .SetDelay(0.3f)
                                 .OnComplete(() =>
                                             {
                                                 entry.gameObject.SetActive(false);
                                                 scoreEntry.UpdateVisuals();
                                             }));
            sequence.Append(scoreEntry.transform.DOPunchPosition(Vector3.right * punchAmount, punchTime));
            sequence.Append(scoreEntry.transform.DOMove(pos, returnTime)
                                      .SetEase(returnEase)
                                      .SetDelay(0.3f));
            sequence.AppendInterval(0.5f);
            sequence.Insert(2, fadeImage.DOFade(0, fadeTime)
                                        .SetEase(fadeEase)
                                        .OnComplete(() => fadeImage.raycastTarget = false));

            sequence.OnComplete(() =>
                                {
                                    scoreEntry.transform.SetParent(parent);
                                    onSequenceComplete?.Invoke();
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
    }
}

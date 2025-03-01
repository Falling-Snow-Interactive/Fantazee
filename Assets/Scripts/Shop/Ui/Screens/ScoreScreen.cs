using System;
using System.Collections.Generic;
using DG.Tweening;
using Fantazee.Scores.Ui.ScoreEntries;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Screens
{
    public abstract class ScoreScreen : ShopScreen
    {
        [Header("Select animation")]
        
        [SerializeField]
        private float selectTime = 0.6f;

        [SerializeField]
        private Ease selectEase = Ease.InCubic;
        
        [SerializeField]
        private float chargeTime = 0.2f;
        
        [SerializeField]
        private Ease chargeEase = Ease.InCubic;

        [SerializeField]
        private float chargeOffset = 200f;

        [FormerlySerializedAs("entryTime")]
        [SerializeField]
        private float purchaseTime = 0.5f;
        
        [FormerlySerializedAs("entryEase")]
        [SerializeField]
        private Ease purchaseEase = Ease.InCubic;

        [FormerlySerializedAs("entryOffset")]
        [SerializeField]
        private float purchaseOffset = -150f;

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
        protected List<ShopScoreEntry> scoreEntries = new();

        [SerializeField]
        private Transform selectedSocket;

        [SerializeField]
        protected Transform animGroup;

        [SerializeField]
        protected Image fadeImage;
        
        // Audio
        private EventInstance swooshSfx;

        protected virtual void Awake()
        {
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;
            fadeImage.raycastTarget = false;
            
            transform.localPosition = hidePos;
        }

        private void Start()
        {
            swooshSfx = RuntimeManager.CreateInstance(ShopSettings.Settings.SwooshSfx);
        }

        protected abstract bool Apply(ScoreEntry scoreEntry);

        protected void ScoreSelectSequence(Transform purchaseEntry, 
                                           ScoreEntry scoreEntry, 
                                           Action onComplete = null)
        {
            

            Vector3 pos = scoreEntry.transform.position;
            Transform parent = scoreEntry.transform.parent;
            scoreEntry.transform.SetParent(animGroup, true);
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(scoreEntry.transform.DOMove(selectedSocket.position, selectTime)
                                      .SetEase(selectEase)
                                      .SetDelay(0.15f)
                                      .OnStart(() => swooshSfx.start()));
            sequence.Insert(0, fadeImage.DOFade(fadeAmount, fadeTime)
                                        .SetEase(fadeEase)
                                        .OnStart(() => fadeImage.raycastTarget = true));
            sequence.Append(purchaseEntry.DOMove(purchaseEntry.position + Vector3.left * chargeOffset, chargeTime)
                                         .SetEase(chargeEase)
                                         .OnStart(() =>
                                                  {
                                                      RuntimeManager.PlayOneShot(ShopSettings.Settings.ChargeSfx);
                                                  }));
            sequence.Append(purchaseEntry.DOMove(selectedSocket.position + Vector3.right * purchaseOffset, purchaseTime)
                                         .SetEase(purchaseEase)
                                         .OnStart(() => swooshSfx.start())
                                         .OnComplete(() =>
                                                     {
                                                         purchaseEntry.gameObject.SetActive(false);
                                                         if (!Apply(scoreEntry))
                                                         {
                                                             Debug.LogWarning("Shop: Cannot apply spell. Returning to shop.");
                                                             onComplete?.Invoke();
                                                             return;
                                                         }
                                                         scoreEntry.UpdateVisuals();
                                                     }));
            sequence.Append(scoreEntry.transform.DOPunchPosition(Vector3.right * punchAmount, punchTime)
                                      .OnStart(() => RuntimeManager.PlayOneShot(ShopSettings.Settings.UpgradeSfx)));
            sequence.Append(scoreEntry.transform.DOMove(pos, returnTime)
                                      .OnStart(() => swooshSfx.start())
                                      .SetEase(returnEase)
                                      .SetDelay(0.3f));
            sequence.AppendInterval(0.5f);
            sequence.Insert(2, fadeImage.DOFade(0, fadeTime)
                                        .SetEase(fadeEase)
                                        .OnComplete(() => fadeImage.raycastTarget = false));

            sequence.OnComplete(() =>
                                {
                                    scoreEntry.transform.SetParent(parent);
                                    onComplete?.Invoke();
                                });
            sequence.Play();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Fantazee.Scores.Ui.Buttons;
using Fantazee.Shop.Settings;
using Fantazee.Shop.Ui.Entries;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace Fantazee.Shop.Ui.Screens
{
    public abstract class ScoreScreen : ShopScreen
    {
        [Header("Score References")]
        
        [SerializeField]
        protected List<ShopScoreButton> scoreEntries = new();

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

        protected abstract bool Apply(ScoreButton scoreButton);

        protected void ScoreSelectSequence(Transform purchaseEntry, 
                                           ScoreButton scoreButton, 
                                           Action onComplete = null)
        {
            // ShopSettings settings = ShopSettings.Settings;
            //
            // Vector3 pos = scoreButton.transform.position;
            // Transform parent = scoreButton.transform.parent;
            // scoreButton.transform.SetParent(animGroup, true);
            //
            // Sequence sequence = DOTween.Sequence();
            //
            // // Selected score to upgrade area
            // Vector3 purchasePos = purchaseEntry.position;
            // Vector3 selectedPos = selectedSocket.position;
            // Vector3 mid = (purchasePos + selectedPos) / 2f;
            // Vector3 purchaseChargePos = purchasePos + Vector3.left * settings.ChargeOffset;
            // Vector3 selectedChargePos = selectedPos + Vector3.right * settings.ChargeOffset;
            //
            // sequence.Append(scoreButton.transform.DOMove(selectedPos, settings.SelectTime)
            //                           .SetEase(settings.SelectEase)
            //                           .OnStart(() => swooshSfx.start()));
            // // Fade background
            // sequence.Insert(0, fadeImage.DOFade(settings.FadeAmount, settings.FadeTime)
            //                             .SetEase(settings.FadeEase)
            //                             .OnStart(() => fadeImage.raycastTarget = true));
            //
            // // // Charge up the scores
            // float chargeInsertTime = settings.ChargeTime + 0.25f;
            // sequence.Insert(chargeInsertTime, purchaseEntry.DOMove(purchaseChargePos, settings.ChargeTime)
            //                                                .SetEase(settings.ChargeEase)
            //                                                .OnStart(() =>
            //                                                         {
            //                                                             RuntimeManager.PlayOneShot(ShopSettings.Settings.ChargeSfx);
            //                                                         }));
            // sequence.Insert(chargeInsertTime, scoreButton.transform.DOMove(selectedChargePos, settings.ChargeTime)
            //                                             .SetEase(settings.ChargeEase));
            // sequence.Insert(chargeInsertTime, purchaseEntry.DOShakeRotation(settings.ChargeTime,
            //                                                                 new Vector3(0, 0, 3),
            //                                                                 30,
            //                                                                 90f,
            //                                                                 false,
            //                                                                 ShakeRandomnessMode.Full)
            //                                                .SetEase(Ease.OutQuad));
            // sequence.Insert(chargeInsertTime, scoreButton.transform.DOShakeRotation(settings.ChargeTime, 
            //                                                 new Vector3(0, 0, 3),
            //                                                 30,
            //                                                 90f,
            //                                                 false,
            //                                                 ShakeRandomnessMode.Full)
            //                                             .SetEase(Ease.OutQuad));
            //
            // // Move in to combine
            // float purchaseInsertTime = chargeInsertTime + settings.ChargeTime;
            // sequence.Insert(purchaseInsertTime, purchaseEntry.DOMove(mid, settings.PurchaseTime)
            //                                                  .SetEase(settings.PurchaseEase)
            //                                                  .OnStart(() => swooshSfx.start())
            //                                                  .OnComplete(() =>
            //                                                              {
            //                                                                  purchaseEntry.gameObject.SetActive(false);
            //                                                                  if (!Apply(scoreButton))
            //                                                                  {
            //                                                                      Debug.LogWarning("Shop: Cannot apply spell. Returning to shop.");
            //                                                                      onComplete?.Invoke();
            //                                                                      return;
            //                                                                  }
            //                                                                  scoreButton.UpdateVisuals();
            //                                                              }));
            // sequence.Insert(purchaseInsertTime, scoreButton.transform.DOMove(mid, settings.PurchaseTime)
            //                                               .SetEase(settings.PurchaseEase));
            //
            // sequence.Append(scoreButton.transform.DOPunchScale(Vector3.one * 0.5f, 0.3f, 5, 1f));
            //
            // float returnTime = purchaseInsertTime + settings.PurchaseTime + 1;
            // sequence.Insert(returnTime, scoreButton.transform.DOMove(pos, settings.ReturnTime)
            //                           .OnStart(() => swooshSfx.start())
            //                           .SetEase(settings.ReturnEase)
            //                           .SetDelay(0.3f));
            // sequence.Insert(returnTime, fadeImage.DOFade(0, settings.FadeTime)
            //                             .SetEase(settings.FadeEase)
            //                             .OnComplete(() => fadeImage.raycastTarget = false));
            //
            // sequence.AppendInterval(0.5f);
            //
            // sequence.OnComplete(() =>
            //                     {
            //                         scoreButton.transform.SetParent(parent);
            //                         onComplete?.Invoke();
            //                     });
            // sequence.Play();
        }
    }
}

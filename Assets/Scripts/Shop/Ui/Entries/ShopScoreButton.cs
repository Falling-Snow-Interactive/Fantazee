using System;
using DG.Tweening;
using Fantazee.Currencies.Ui;
using Fantazee.Scores.Instance;
using Fantazee.Scores.Ui.Buttons;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopScoreButton : ScoreButton
    {
        [Header("Cost")]
        
        [SerializeField]
        private CurrencyEntryUi currencyEntryUi;
        
        [Header("Score References")]

        [SerializeField]
        private Image borderImage;
        
        public void Initialize(ScoreInstance score, Action<ShopScoreButton> onSelect)
        {
            base.Initialize(score, _ =>
                                          {
                                              onSelect?.Invoke(this);
                                          });
            currencyEntryUi.SetCurrency(score.Data.Cost);
        }
        
        public void PlayCantAfford()
        {
            DOTween.Complete(transform);
            DOTween.Complete(borderImage);
            DOTween.Complete(NameText);
            
            transform.DOPunchScale(Vector3.one * -0.1f, 0.2f, 10, 1f);
            
            Color b1 = borderImage.color;
            Color b2 = Color.red;
            b2.a = b1.a;
            borderImage.color = b2;
            borderImage.DOColor(b1, 0.2f);

            Color t1 = NameText.color;
            Color t2 = Color.red;
            t2.a = t1.a;
            NameText.color = t2;
            NameText.DOColor(t1, 0.2f);
        }
    }
}
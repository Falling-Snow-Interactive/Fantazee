using DG.Tweening;
using Fantazee.Scores.Ui.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.Entries
{
    public class ShopScoreButton : ScoreButton
    {
        [Header("Score References")]

        [SerializeField]
        private Image borderImage;
        
        public void PlayCantAfford()
        {
            DOTween.Complete(transform);
            DOTween.Complete(borderImage);
            DOTween.Complete(nameText);
            
            transform.DOPunchScale(Vector3.one * -0.1f, 0.2f, 10, 1f);
            
            Color b1 = borderImage.color;
            Color b2 = Color.red;
            b2.a = b1.a;
            borderImage.color = b2;
            borderImage.DOColor(b1, 0.2f);

            Color t1 = nameText.color;
            Color t2 = Color.red;
            t2.a = t1.a;
            nameText.color = t2;
            nameText.DOColor(t1, 0.2f);
        }
    }
}
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public class ScoreEntrySpellTooltip : MonoBehaviour
    {
        [Header("References")]
        
        [SerializeField]
        private Transform root;
        
        [SerializeField]
        private new TMP_Text name;
        
        [SerializeField]
        private TMP_Text desc;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private Vector3 offset = new(0f, 100f, 0f);
        
        [SerializeField]
        private float time = 5f;
        
        [SerializeField]
        private Ease showEase = Ease.OutBounce;
        
        [SerializeField]
        private Ease hideEase = Ease.OutBounce;
        
        public void FillTooltip(ScoreEntrySpell scoreSpell)
        {
            name.text = scoreSpell.Data.LocName.GetLocalizedString();
            desc.text = scoreSpell.Data.LocDesc.GetLocalizedString();
            icon.sprite = scoreSpell.Data.Icon;
        }
        
        public void Show(ScoreEntrySpell scoreSpell, bool force = false)
        {
            DOTween.Complete(root);
            
            root.gameObject.SetActive(true);
            FillTooltip(scoreSpell);
            
            if (force)
            {
                root.transform.localPosition = offset;
            }

            root.transform.DOLocalMove(offset, time)
                .SetEase(showEase);
        }

        public void Hide(bool force = false)
        {
            DOTween.Complete(root);

            if (force)
            {
                root.transform.localPosition = Vector3.zero;
                root.gameObject.SetActive(false);
            }
            
            root.transform.DOLocalMove(Vector3.zero, time)
                .SetEase(hideEase)
                .OnComplete(() =>
                            {
                                root.gameObject.SetActive(false);
                            });
        }
    }
}
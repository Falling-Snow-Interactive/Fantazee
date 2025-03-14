using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Spells.Ui
{
    public class SpellTooltip : MonoBehaviour
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

        public void Initialize(SpellInstance spell)
        {
            FillTooltip(spell);
        }

        private void FillTooltip(SpellInstance spell)
        {
            name.text = spell.Data.Name;
            desc.text = spell.Data.Description;
            icon.sprite = spell.Data.Icon;
        }
        
        public void Show(bool force = false)
        {
            root.gameObject.SetActive(true);
            return;
            
            DOTween.Complete(root);
            
            root.gameObject.SetActive(true);
            // FillTooltip(scoreSpellButton);
            
            if (force)
            {
                root.transform.localPosition = offset;
            }

            root.transform.DOLocalMove(offset, time)
                .SetEase(showEase);
        }

        public void Hide(bool force = false)
        {
            root.gameObject.SetActive(false);
            return;
            
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
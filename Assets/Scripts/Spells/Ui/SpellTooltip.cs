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
        }
        
        public void Show()
        {
            root.gameObject.SetActive(true);
        }

        public void Hide()
        {
            root.gameObject.SetActive(false);
        }
    }
}
using TMPro;
using UnityEngine.UI;

namespace Fantazee.Spells.Ui
{
    public class SpellTooltip : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private new TMP_Text name;
        
        [SerializeField]
        private TMP_Text desc;
        
        [SerializeField]
        private RectTransform layoutGroup;

        public void Initialize(SpellInstance spell)
        {
            icon.sprite = spell.Data.Icon;
            name.text = spell.Data.Name;
            desc.text = spell.Data.Description;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
        }
    }
}
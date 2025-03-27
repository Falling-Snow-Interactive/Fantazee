using System;
using DG.Tweening;
using Fantazee.Ui.Buttons;
using UnityEngine.UI;

namespace Fantazee.Spells.Ui
{
    public class SpellButton : SimpleButton
    {
        private Action<SpellButton> onClick;
        
        [Header("Spell")]
        
        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private SpellTooltip tooltip;
        
        [Header("Animations")]
        
        [Header("Punch")]

        [SerializeField]
        private float punchTime = 0.3f;

        [SerializeField]
        private Vector3 punchPosition;
        
        [SerializeField]
        private Vector3 punchRotation;
        
        [SerializeField]
        private Vector3 punchScale;

        public void Initialize(SpellInstance spell, Action<SpellButton> onSelect)
        {
            this.onClick = onSelect;
            
            this.spell = spell;
            icon.sprite = spell.Data.Icon;

            tooltip.Initialize(spell);
            tooltip?.Hide();
        }

        public void Activate(Action<SpellButton> onSelect)
        {
            this.onClick = onSelect;
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one * 2f, 0.2f);
        }

        public void Deactivate()
        {
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one, 0.2f);
        }

        public override void OnClick()
        {
            onClick?.Invoke(this);
        }

        public void SetTooltip(bool set)
        {
            if (set && spell.Data.Type != SpellType.spell_none)
            {
                // tooltip.transform.SetParent(transform.parent.parent, true);
                // tooltip.transform.SetAsLastSibling();
                tooltip?.Show(Spell);
            }
            else
            {
                // tooltip.transform.SetParent(transform, true);
                tooltip?.Hide();
            }
        }

        public void Punch(Action onComplete = null)
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(transform.DOPunchPosition(punchPosition, punchTime));
            sequence.Insert(0, transform.DOPunchRotation(punchRotation, punchTime));
            sequence.Insert(0, transform.DOPunchScale(punchScale, punchTime));
            
            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.Play();
        }

        public override void OnPointerEnter()
        {

        }

        public override void OnSelect()
        {
            
        }

        public override void OnPointerExit()
        {

        }

        public override void OnDeselect()
        {
            
        }
    }
}
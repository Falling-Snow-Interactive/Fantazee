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

        private bool canSelect = false;

        public void Initialize(SpellInstance spell, Action<SpellButton> onSelect)
        {
            this.onClick = onSelect;
            
            this.spell = spell;
            icon.sprite = spell.Data.Icon;
        }

        public void Activate(Action<SpellButton> onSelect)
        {
            this.onClick = onSelect;
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one * 2f, 0.2f);
            canSelect = true;
        }

        public void Deactivate()
        {
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one, 0.2f);
            canSelect = false;
        }

        public override void OnClick()
        {
            base.OnClick();
            onClick?.Invoke(this);
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

        // public override void OnSelect()
        // {
        //     if (canSelect)
        //     {
        //         base.OnSelect();
        //     }
        // }
    }
}
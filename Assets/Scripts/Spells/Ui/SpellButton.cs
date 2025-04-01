using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Fantazee.Ui.Buttons;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fantazee.Spells.Ui
{
    public class SpellButton : SimpleButton
    {
        private Action<SpellButton> onClickCallback;
        
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

        protected bool canSelect = false;

        public void Initialize(SpellInstance spell, Action<SpellButton> onClickCallback)
        {
            this.onClickCallback = onClickCallback;
            
            this.spell = spell;
            icon.sprite = spell.Data.Icon;
        }

        public Tweener Activate(Action<SpellButton> onClickCallback)
        {
            this.onClickCallback = onClickCallback;
            DOTween.Complete(transform);

            Tweener t = transform.DOScale(Vector3.one * 2f, 0.2f);
            canSelect = true;

            return t;
        }

        public void Deactivate()
        {
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one, 0.2f);
            canSelect = false;
        }

        public override void OnClick()
        {
            if (canSelect)
            {
                base.OnClick();
                onClickCallback?.Invoke(this);
            }
        }

        public override void OnSelect()
        {
            if (canSelect)
            {
                base.OnSelect();
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
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Spells.Ui
{
    public class SpellButton : MonoBehaviour
    {
        private Action<SpellButton> onSelect;

        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Button button;
        
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

        public void Initialize(SpellInstance spell)
        {
            this.spell = spell;
            icon.sprite = spell.Data.Icon;

            tooltip?.Hide(true);
        }

        public void Activate(Action<SpellButton> onSelect)
        {
            this.onSelect = onSelect;
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one * 2f, 0.2f);
        }

        public void Deactivate()
        {
            DOTween.Complete(transform);
            
            transform.DOScale(Vector3.one, 0.2f);
        }

        public void OnSelect()
        {
            onSelect?.Invoke(this);
        }

        public void SetTooltip(bool set)
        {
            if (set && spell.Data.Type != SpellType.spell_none)
            {
                tooltip?.Show(this);
            }
            else
            {
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
    }
}
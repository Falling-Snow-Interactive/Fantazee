using System;
using DG.Tweening;
using Fantazee.Spells;
using Fantazee.Spells.Instance;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public class ScoreEntrySpell : MonoBehaviour
    {
        private Action<SpellInstance> onSelect;

        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private ScoreEntrySpellTooltip tooltip;
        
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

        public void Activate(Action<SpellInstance> onSelect)
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
            onSelect?.Invoke(spell);
        }

        public void SetTooltip(bool set)
        {
            if (spell.Data.Type == SpellType.None)
            {
                return;
            }
            
            if (set)
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
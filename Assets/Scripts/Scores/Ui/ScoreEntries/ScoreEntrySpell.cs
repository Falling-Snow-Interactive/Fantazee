using System;
using DG.Tweening;
using Fantazee.Spells.Instance;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public class ScoreEntrySpell : MonoBehaviour
    {
        private Action<int> onSelect;
        private int i;

        [SerializeReference]
        private SpellInstance spell;
        public SpellInstance Spell => spell;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private ScoreEntrySpellTooltip tooltip;

        public void Initialize(int i, SpellInstance spell)
        {
            this.i = i;
            icon.sprite = spell.Data.Icon;

            tooltip?.Hide(true);
        }

        public void Activate(Action<int> onSelect)
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
            onSelect?.Invoke(i);
        }

        public void SetTooltip(bool set)
        {
            if (set)
            {
                tooltip?.Show(this);
            }
            else
            {
                tooltip?.Hide();
            }
        }
    }
}
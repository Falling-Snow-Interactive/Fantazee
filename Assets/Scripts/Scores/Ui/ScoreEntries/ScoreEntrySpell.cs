using System;
using DG.Tweening;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui.ScoreEntries
{
    public class ScoreEntrySpell : MonoBehaviour
    {
        private Action<int> onSelect;
        private SpellType spell;
        private int i;

        [SerializeReference]
        private SpellData data;
        public SpellData Data => data;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private ScoreEntrySpellTooltip tooltip;

        public void Initialize(int i, SpellType spell)
        {
            this.i = i;
            if (SpellSettings.Settings.TryGetSpell(spell, out data))
            {
                icon.sprite = data.Icon;
            }

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
            if (data.Type == SpellType.None)
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
    }
}
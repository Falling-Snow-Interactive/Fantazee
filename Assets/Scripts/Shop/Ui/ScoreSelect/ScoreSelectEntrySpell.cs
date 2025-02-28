using System;
using DG.Tweening;
using Fantazee.Spells;
using Fantazee.Spells.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.ScoreSelect
{
    public class ScoreSelectEntrySpell : MonoBehaviour
    {
        private Action<int> onSelect;
        private SpellType spell;
        private int i;

        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private Transform iconGroup;
        
        [SerializeField]
        private TMP_Text text;
        
        [SerializeField]
        private Button button;

        public void Initialize(int i, SpellType spell)
        {
            this.i = i;
            if (SpellSettings.Settings.TryGetSpell(spell, out var data))
            {
                icon.sprite = data.Icon;
                text.text = data.LocName.GetLocalizedString();
            }
        }

        public void Activate(Action<int> onSelect)
        {
            this.onSelect = onSelect;
            DOTween.Complete(iconGroup.transform);
            
            iconGroup.transform.DOScale(Vector3.one * 1.2f, 0.2f);
        }

        public void Deactivate()
        {
            DOTween.Complete(iconGroup.transform);
            
            iconGroup.transform.DOScale(Vector3.one, 0.2f);
        }

        public void OnSelect()
        {
            onSelect?.Invoke(i);
        }
    }
}
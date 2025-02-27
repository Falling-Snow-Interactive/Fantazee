using System;
using Fantazee.Battle.Settings;
using Fantazee.Scores;
using Fantazee.Scores.Information;
using Fantazee.Spells;
using Fantazee.Spells.Data;
using Fantazee.Spells.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Shop.Ui.ScoreSelect
{
    public class ScoreSelectEntry : MonoBehaviour
    {
        private Action<ScoreSelectEntry> onSelect;
        
        [SerializeReference]
        private Score score;
        public Score Score => score;
        
        [Header("References")]

        [SerializeField]
        private Image icon;

        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private TMP_Text spellText;

        public void Initialize(Score score, Action<ScoreSelectEntry> onSelect)
        {
            this.score = score;
            this.onSelect = onSelect;

            UpdateSpell();
        }

        public void UpdateSpell()
        {
            if (BattleSettings.Settings.ScoreInformation.TryGetInformation(score.Type, out ScoreInformation scoreInfo))
            {
                scoreText.text = scoreInfo.LocName.GetLocalizedString();
            }

            if (SpellSettings.Settings.TryGetSpell(score.Spell, out SpellData spellInfo))
            {
                icon.sprite = spellInfo.Icon;
                spellText.text = spellInfo.LocName.GetLocalizedString();
            }
        }

        public void Select()
        {
            onSelect?.Invoke(this);
        }
    }
}

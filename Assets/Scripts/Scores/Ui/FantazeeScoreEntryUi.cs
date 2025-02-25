using System.Collections.Generic;
using Fantazee.Battle.Score;
using Fantazee.Spells.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Scores.Ui
{
    public class FantazeeScoreEntryUi : ScoreEntry
    {
        [SerializeField]
        private List<Image> spellIcons = new List<Image>();

        public override void Initialize(BattleScore score)
        {
            base.Initialize(score);

            if (score.Score is FantazeeScore fantazeeScore)
            {
                for (int i = 0; i < spellIcons.Count; i++)
                {
                    Image icon = spellIcons[i];
                    if (SpellSettings.Settings.TryGetSpell(fantazeeScore.Spells[i], out var spell))
                    {
                        icon.sprite = spell.Icon;
                    }
                }
            }
        }
    }
}

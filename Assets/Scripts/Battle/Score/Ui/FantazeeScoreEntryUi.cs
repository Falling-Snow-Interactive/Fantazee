using System;
using System.Collections.Generic;
using Fantazee.Scores;
using Fantazee.Spells.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fantazee.Battle.Score.Ui
{
    public class FantazeeScoreEntryUi : ScoreEntry
    {
        public override void Initialize(BattleScore score, Action<ScoreEntry> onSelect)
        {
            base.Initialize(score, onSelect);

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

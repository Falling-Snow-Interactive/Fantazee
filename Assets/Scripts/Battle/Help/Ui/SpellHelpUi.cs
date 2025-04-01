using Fantazee.Instance;
using Fantazee.Scores.Instance;
using Fantazee.Spells;
using Fantazee.Spells.Ui;

namespace Fantazee.Battle.Help.Ui
{
    public class SpellHelpUi : MonoBehaviour
    {
        [Header("Prefabs")]
        
        [SerializeField] 
        private SpellTooltip spellTooltipPrefab;

        [Header("References")]

        [SerializeField]
        private Transform content;

        private void Start()
        {
            Dictionary<SpellType, SpellInstance> spells = new();
            foreach (ScoreInstance score in GameInstance.Current.Character.Scoresheet.Scores)
            {
                foreach (SpellInstance spell in score.Spells)
                {
                    if (spell.Data.Type == SpellType.spell_none)
                    {
                        continue;
                    }
                    
                    spells.TryAdd(spell.Data.Type, spell);
                }
            }

            foreach (SpellInstance spell in spells.Values)
            {
                SpellTooltip tooltip = Instantiate(spellTooltipPrefab, content);
                tooltip.Initialize(spell);
            }
        }
    }
}

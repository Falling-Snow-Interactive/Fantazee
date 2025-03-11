using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_05_Overflow", 
                     fileName = "Spell_05_Overflow", order = 5)]
    public class OverflowSpellData : SpellData
    {
        public override SpellType Type => SpellType.Overflow;

        [Header("Overflow")]
        
        [SerializeField]
        private float spreadTime = 0.2f;
        public float SpreadTime => spreadTime;
    }
}

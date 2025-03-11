using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_03_Heal", 
                     fileName = "Spell_03_Heal", order = 3)]
    public class HealSpellData : SpellData
    {
        public override SpellType Type => SpellType.Heal;
     
        [Header("Heal")]
        
        [Range(0,2)]
        [SerializeField]
        private float healingModifier = 1;
        public float HealingModifier => healingModifier;
    }
}

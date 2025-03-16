using UnityEngine;
using UnityEngine.Serialization;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_03_Heal", 
                     fileName = "Spell_03_Heal", order = 3)]
    public class HealSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_03_heal;
     
        [Header("Heal")]
        
        [Range(0,2)]
        [SerializeField]
        private float healMod = 1;
        public float HealMod => healMod;
    }
}

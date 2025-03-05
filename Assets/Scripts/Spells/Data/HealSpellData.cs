using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Heal")]
    public class HealSpellData : SpellData
    {
        public override SpellType Type => SpellType.Heal;
        
        [Range(0,2)]
        [SerializeField]
        private float healingModifier = 1;
        public float HealingModifier => healingModifier;
    }
}

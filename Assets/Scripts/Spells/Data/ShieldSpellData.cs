using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_02_Shield", 
                     fileName = "Spell_02_Shield", order = 2)]
    public class ShieldSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_02_shield;

        [Header("Shield")]

        [Range(0f, 2f)]
        [SerializeField]
        private float shieldMod = 1f;
        public float ShieldMod => shieldMod;
    }
}
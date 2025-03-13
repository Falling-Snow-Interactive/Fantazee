using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_04_Fireball", 
                     fileName = "Spell_04_Fireball", order = 4)]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.Fireball;

        [Header("Fireball")]

        [Range(0f, 2f)]
        [SerializeField]
        private float damageMod = 1f;
        public float DamageMod => damageMod;
    }
}

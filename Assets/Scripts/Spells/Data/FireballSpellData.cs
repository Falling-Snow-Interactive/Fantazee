using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_04_Fireball", 
                     fileName = "Spell_04_Fireball", order = 4)]
    public class FireballSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_04_fireball;

        [Header("Fireball")]

        [Range(0f, 2f)]
        [SerializeField]
        private float damageMod = 1f;
        public float DamageMod => damageMod;

        [Range(0, 1f)]
        [SerializeField]
        private float burnRoll = 0.2f;
        public float BurnRoll => burnRoll;

        [SerializeField]
        private int turns = 2;
        public int Turns => turns;
    }
}

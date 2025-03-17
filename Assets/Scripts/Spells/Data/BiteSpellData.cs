using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Spell_08_Bite", 
                     fileName = "Spell_08_Bite_Data", order = 8)]
    public class BiteSpellData : SpellData
    {
        public override SpellType Type => SpellType.spell_08_bite;

        [Header("Bite")]

        [Range(0, 1f)]
        [SerializeField]
        private float roll = 0.2f;
        public float Roll => roll;

        [SerializeField]
        private int turns = 2;
        public int Turns => turns;
    }
}

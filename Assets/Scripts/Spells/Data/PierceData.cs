using UnityEngine;

namespace Fantazee.Spells.Data
{
    [CreateAssetMenu(menuName = "Spells/Pierce")]
    public class PierceData : SpellData
    {
        public override SpellType Type => SpellType.Pierce;

        [Range(0,2)]
        [SerializeField]
        private float firstEnemyMod = 0.75f;
        public float FirstEnemyMod => firstEnemyMod;

        [Range(0, 2)]
        [SerializeField]
        private float secondEnemyMod = 0.25f;
        public float SecondEnemyMod => secondEnemyMod;
    }
}

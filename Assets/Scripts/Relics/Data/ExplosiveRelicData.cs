using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "Explosive Data", menuName = "Relics/Explosive")]
    public class ExplosiveRelicData : RelicData
    {
        public override RelicType Type => RelicType.relic_01_explosive;

        [SerializeField]
        private int damage = 1;
        public int Damage => damage;

        [SerializeField]
        private int number = 3;
        public int Number => number;
    }
}
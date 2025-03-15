using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "Vampire Fang Data", menuName = "Relics/Vampire Fang")]
    public class VampireFangRelicData : RelicData
    {
        public override RelicType Type => RelicType.relic_04_vampire_fang;

        [Tooltip("Damage that acts as life steal. \nEx: 10% => 0.1")]
        [SerializeField]
        private float lifeSteal = 0.1f;
        public float LifeSteal => lifeSteal;

        [SerializeField]
        private int minLifeStealAmount = 1;
        public int MinLifeStealAmount => minLifeStealAmount;
    }
}
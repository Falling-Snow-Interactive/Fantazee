using UnityEngine;

namespace Fantazee.StatusEffects.Data
{
    [CreateAssetMenu(menuName = "Status Effects/Poison", fileName = "status_02_poison_data", order = 2)]
    public class PoisonStatusData : StatusEffectData
    {
        public override StatusEffectType Type => StatusEffectType.status_02_poison;
    }
}
using UnityEngine;

namespace Fantazee.StatusEffects.Data
{
    [CreateAssetMenu(menuName = "Status Effects/Bleed", fileName = "status_01_bleed")]
    public class BleedStatusData : StatusEffectData
    {
        public override StatusEffectType Type => StatusEffectType.status_01_bleed;
    }
}
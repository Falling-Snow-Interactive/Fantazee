using FMODUnity;
using UnityEngine;

namespace Fantazee.StatusEffects.Data
{
    [CreateAssetMenu(menuName = "Status Effects/Burn", fileName = "status_00_burn_data")]
    public class BurnStatusData : StatusEffectData
    {
        public override StatusEffectType Type => StatusEffectType.status_00_burn;
        
        [SerializeField]
        private GameObject burnEffectPrefab;
        public GameObject BurnEffectPrefab => burnEffectPrefab;

        [SerializeField]
        private EventReference burnSfx;
        public EventReference BurnSfx => burnSfx;
    }
}
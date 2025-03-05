using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "Lucky Data", menuName = "Relics/Lucky")]
    public class LuckyRelicData : RelicData
    {
        public override RelicType Type => RelicType.Lucky;
    }
}
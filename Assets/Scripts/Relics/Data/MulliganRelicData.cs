using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "Mulligan Data", menuName = "Relics/Mulligan")]
    public class MulliganRelicData : RelicData
    {
        public override RelicType Type => RelicType.Mulligan;
    }
}
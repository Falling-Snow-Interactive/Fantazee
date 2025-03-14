using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "No Vacancy Data", menuName = "Relics/No Vacancy")]
    public class NoVacancyRelicData : RelicData
    {
        public override RelicType Type => RelicType.relic_03_no_vacancy;
    }
}
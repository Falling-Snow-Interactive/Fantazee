using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores.Instance;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class NoVacancyRelicInstance : RelicInstance
    {
        private NoVacancyRelicData noVacancyData;
        
        public NoVacancyRelicInstance(NoVacancyRelicData data, CharacterInstance character) : base(data, character)
        {
            noVacancyData = data;
        }

        public override void Enable()
        {
            BattleController.Scored += OnScored;
        }

        public override void Disable()
        {
            BattleController.Scored -= OnScored;
        }

        private void OnScored(BattleScore battleScore)
        {
            if (battleScore.Score.Data.Type == noVacancyData.Score) 
            {
                Debug.Log($"No Vacancy: Activated. {BattleController.Instance.RemainingRolls} -> {BattleController.Instance.RemainingRolls + 1}");
                BattleController.Instance.RemainingRolls++;
            }
        }
    }
}
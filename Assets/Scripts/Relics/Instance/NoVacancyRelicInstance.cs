using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class NoVacancyRelicInstance : RelicInstance
    {
        public NoVacancyRelicInstance(RelicData data, CharacterInstance character) : base(data, character)
        {
            BattleController.Scored += OnScored;
        }

        private void OnScored(BattleScore battleScore)
        {
            if (battleScore.Score.Type == ScoreType.FullHouse)
            {
                Debug.Log($"No Vacancy: Activated. {BattleController.Instance.RemainingRolls} -> {BattleController.Instance.RemainingRolls + 1}");
                BattleController.Instance.RemainingRolls++;
            }
        }
    }
}
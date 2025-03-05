using Fantazee.Battle;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores;

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
                BattleController.Instance.RemainingRolls++;
            }
        }
    }
}
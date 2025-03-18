using System;
using Fantazee.Battle;
using Fantazee.Battle.CallbackReceivers;
using Fantazee.Battle.Characters.Player;
using Fantazee.Battle.Score;
using Fantazee.Instance;
using Fantazee.Relics.Data;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Relics.Instance
{
    public class NoVacancyRelicInstance : RelicInstance
    {
        private readonly NoVacancyRelicData noVacancyData;
        
        public NoVacancyRelicInstance(NoVacancyRelicData data, CharacterInstance character) : base(data, character)
        {
            noVacancyData = data;
        }

        public override void Enable()
        {
            BattleController.BattleStarted += OnBattleStart;
            BattleController.BattleEnded += OnBattleEnd;
        }

        public override void Disable()
        {
            BattleController.BattleStarted -= OnBattleStart;
            BattleController.BattleEnded -= OnBattleEnd;
        }

        private void OnBattleStart()
        {
            BattleController.Instance.Player.Scored += OnScored;
        }

        private void OnBattleEnd()
        {
            
        }

        private void OnScored(ScoreResults results)
        {
            if (results.Score.Data.Type == noVacancyData.Score) 
            {
                Debug.Log($"No Vacancy: Activated. Rolls: {BattleController.Instance.Player.RollsRemaining} " +
                          $"-> {BattleController.Instance.Player.RollsRemaining + 1}");
                BattleController.Instance.Player.RollsRemaining++;
            }
        }
    }
}
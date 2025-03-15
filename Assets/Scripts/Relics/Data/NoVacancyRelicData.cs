using System;
using System.Collections.Generic;
using Fantazee.Scores;
using Fantazee.Scores.Settings;
using UnityEngine;

namespace Fantazee.Relics.Data
{
    [CreateAssetMenu(fileName = "No Vacancy Data", menuName = "Relics/No Vacancy")]
    public class NoVacancyRelicData : RelicData
    {
        public override RelicType Type => RelicType.relic_03_no_vacancy;

        [Header("No Vacancy")]

        [SerializeField]
        private ScoreType score;
        public ScoreType Score => score;

        protected override Dictionary<string, string> BuildDescArgs()
        {
            Dictionary<string, string> args = base.BuildDescArgs();
            if (ScoreSettings.Settings.TryGetScore(score, out var data))
            {
                args.Add("Score", $"{data.Name}");
            }
            return args;
        }
    }
}
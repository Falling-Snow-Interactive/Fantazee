using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Run")]
    public class RunScoreData : ScoreData
    {
        [Header("Run")]

        [SerializeField]
        private int run = 3;
        public int Run => run;

        public override ScoreType Type
        {
            get
            {
                return run switch
                       {
                           3 => ScoreType.SmallRun,
                           4 => ScoreType.LargeRun,
                           5 => ScoreType.FullRun,
                           _ => throw new System.NotImplementedException()
                       };
            }
        }

        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new() { { "Run", run.ToString() } };
            return args;
        }
    }
}
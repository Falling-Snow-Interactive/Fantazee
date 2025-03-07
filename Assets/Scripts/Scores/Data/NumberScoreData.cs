using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Number")]
    public class NumberScoreData : ScoreData
    {
        [Header("Number")]

        [Range(1,6)]
        [SerializeField]
        private int number;
        public int Number => number;

        public override ScoreType Type
        {
            get
            {
                return number switch
                       {
                           1 => ScoreType.Ones,
                           2 => ScoreType.Twos,
                           3 => ScoreType.Threes,
                           4 => ScoreType.Fours,
                           5 => ScoreType.Fives,
                           6 => ScoreType.Sixes,
                           _ => throw new ArgumentOutOfRangeException()
                       };
            }
        }

        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new() { { "Number", number.ToString() } };
            return args;
        }
    }
}
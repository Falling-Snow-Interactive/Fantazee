using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fantazee.Scores.Data
{
    [CreateAssetMenu(menuName = "Scores/Kind")]
    public class KindScoreData : ScoreData
    {
        [Header("Kind")]

        [Range(2,5)]
        [SerializeField]
        private int kind = 3;
        public int Kind => kind;

        public override ScoreType Type
        {
            get
            {
                return kind switch
                       {
                           2 => ScoreType.TwoOfAKind,
                           3 => ScoreType.ThreeOfAKind,
                           4 => ScoreType.FourOfAKind,
                           5 => ScoreType.FiveOfAKind,
                           _ => throw new ArgumentOutOfRangeException()
                       };
            }
        }

        protected override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string,string> args = new() { { "Kind", kind.ToString() } };
            return args;
        }
    }
}
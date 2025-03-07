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
        
        public override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string, string> args = new() { { "Number", number.ToString() } };
            return args;
        }
    }
}
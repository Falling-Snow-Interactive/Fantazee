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
        
        public override Dictionary<string, string> GetDescArgs()
        {
            Dictionary<string,string> args = new() { { "Number", "Kind" } };
            return args;
        }
    }
}
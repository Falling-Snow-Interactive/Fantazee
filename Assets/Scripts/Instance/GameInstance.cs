using System;
using System.Collections.Generic;
using ProjectYahtzee.Gameplay.Scores;
using UnityEngine;

namespace ProjectYahtzee.Instance
{
    [Serializable]
    public class GameInstance
    {
        [SerializeField]
        private List<ScoreType> scoreTypes = new List<ScoreType>();
        public List<ScoreType> ScoreTypes => scoreTypes;
    }
}

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
        private List<ScoreCard> scoreCards = new List<ScoreCard>();
        public List<ScoreCard> ScoreCards => scoreCards;
    }
}

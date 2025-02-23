using System;
using Fantazee.Scores;
using UnityEngine;

namespace Fantazee.Characters
{
    [Serializable]
    public class CharacterInstance
    {
        [SerializeField]
        private ScoreTracker scoreTracker;
        public ScoreTracker ScoreTracker => scoreTracker;
    }
}

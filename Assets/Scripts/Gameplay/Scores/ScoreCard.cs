using System;
using UnityEngine;

namespace ProjectYahtzee.Gameplay.Scores
{
    [Serializable]
    public class ScoreCard : ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private string name;
        
        [SerializeField]
        private ScoreType type;
        public ScoreType Type => type;

        [SerializeField]
        private int level;
        public int Level => level;

        public ScoreCard(ScoreType type, int level)
        {
            this.type = type;
            this.level = level;
        }

        public void OnBeforeSerialize()
        {
            name = $"{type} - {level}";
        }

        public void OnAfterDeserialize() { }
    }
}